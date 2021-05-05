using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QPCore.Common;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Helpers;
using QPCore.Model.Accounts;
using QPCore.Service.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace QPCore.Service
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<OrgUser> _orgUsersRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountService(IRepository<OrgUser> orgUsersRepository,
            IRepository<RefreshToken> refreshTokenRepository,
            IRepository<Role> roleRepository,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            IEmailService emailService)
        {
            this._orgUsersRepository = orgUsersRepository;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;
            this._emailService = emailService;
            this._refreshTokenRepository = refreshTokenRepository;
            this._roleRepository = roleRepository;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, string ipAddress)
        {
            var account = _orgUsersRepository.GetQuery()
                 .SingleOrDefault(p => p.LoginName == model.Email);

            if (account == null || !BC.Verify(model.Password, account.Password))
            {
                throw new AppException("Email or password is incorrect");
            }

            var jwtToken = generateJwtToken(account);
            var refreshToken = generateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);

            await _orgUsersRepository.UpdateAsync(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<AuthenticateResponse> RefreshTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = getRefreshToken(token);

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            await _orgUsersRepository.UpdateAsync(account);

            // generate new jwt
            var jwtToken = generateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = getRefreshToken(token);

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _orgUsersRepository.UpdateAsync(account);
        }

        public async Task RegisterAsync(RegisterRequest model, string origin)
        {
            // validate
            if (this._orgUsersRepository.GetQuery().Any(x => x.Email == model.Email))
            {
                throw new AppException("Email has already used in the system. Please use the other one then try again.");
            }

            // map model to new account object
            var account = _mapper.Map<OrgUser>(model);

            // first registered account is an admin
            //var isFirstAccount = _context.Accounts.Count() == 0;
            //account.Role = isFirstAccount ? Role.Admin : Role.User;

            // hash password
            var maxId = 0;
            if (_orgUsersRepository.GetQuery().Any())
            {
                maxId = _orgUsersRepository.GetQuery().Max(p => p.UserId);
            }
            account.UserId = maxId + 1;
            account.Password = BC.HashPassword(model.Password);
            account.Enabled = new BitArray(new bool[] { true });
            account.UseWindowsAuth = new BitArray(new bool[] { false });
            account.OrgId = GlobalConstants.DEFAUTL_ORGANIZATION_ID;
            account.VerificationToken = randomTokenString();
            account.Created = DateTime.UtcNow;

            // save account
            await _orgUsersRepository.AddAsync(account);

            // send email
            sendVerificationEmail(account, origin);
        }

        public AccountResponse GetById(int id)
        {
            var account = _orgUsersRepository.GetQuery().FirstOrDefault(p => p.UserId == id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task VerifyEmailAsync(string token)
        {
            var account = this._orgUsersRepository.GetQuery().SingleOrDefault(x => x.VerificationToken == token);

            if (account == null) throw new AppException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await this._orgUsersRepository.UpdateAsync(account);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequest model, string origin)
        {
            var account = _orgUsersRepository.GetQuery().SingleOrDefault(x => x.Email == model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) throw new AppException("Email is not existed in system.");

            // create reset token that expires after 1 day
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            await _orgUsersRepository.UpdateAsync(account);

            // send email
            sendPasswordResetEmail(account, origin);
        }

        public bool ValidateResetToken(ValidateResetTokenRequest model)
        {
            var result = _orgUsersRepository.GetQuery().Any(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.UtcNow);

            if (!result)
                throw new AppException("Invalid token");

            return true;
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest model)
        {
            var account = _orgUsersRepository.GetQuery().SingleOrDefault(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.UtcNow);

            if (account == null)
                throw new AppException("Invalid token");

            // update password and remove reset token
            account.Password = BC.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _orgUsersRepository.UpdateAsync(account);
        }

        public bool OwnsToken(int userId, string token)
        {
            var query = _orgUsersRepository.GetQuery()
                            .Any(p => p.UserId == userId && p.RefreshTokens.Any(k => k.Token == token));

            return query;
        }

        #region private methods
        private string generateJwtToken(OrgUser account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var claims = new List<Claim>();
            claims.Add(new Claim("id", account.UserId.ToString()));
            claims.Add(new Claim("orgId", account.OrgId.ToString()));
            claims.Add(new Claim("firstname", account.FirstName));
            claims.Add(new Claim("lastname", account.LastName));

            // Get Role
            var roles = _roleRepository.GetQuery()
                            .Where(p => p.UserRoles.Any(r => r.UserClientId == account.UserId))
                            .ToList();

            if (roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleCode));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void removeOldRefreshTokens(OrgUser account)
        {
            var processingList = account.RefreshTokens.ToList();
            processingList.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            account.RefreshTokens = processingList;
        }

        private (RefreshToken, OrgUser) getRefreshToken(string token)
        {
            var account = _orgUsersRepository.GetQuery()
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (account == null)
                throw new AppException("Invalid token");

            var refreshToken = _refreshTokenRepository.GetQuery().SingleOrDefault(x => x.Token == token);
            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");
            return (refreshToken, account);
        }

        private void sendVerificationEmail(OrgUser account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        private void sendPasswordResetEmail(OrgUser account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }

        #endregion
    }
}
