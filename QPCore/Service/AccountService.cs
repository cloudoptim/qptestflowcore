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
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;


        public AccountService(IRepository<OrgUser> orgUsersRepository,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            this._orgUsersRepository = orgUsersRepository;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;
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

        public async Task Register(RegisterRequest model, string origin)
        {
            // validate
            if (this._orgUsersRepository.GetQuery().Any(x => x.Email == model.Email))
            {
                // send already registered error in email to prevent account enumeration
                //sendAlreadyRegisteredEmail(model.Email, origin);
                throw new AppException("Email has already used in the system. Please use the other one then try again.");
            }

            // map model to new account object
            var account = _mapper.Map<OrgUser>(model);

            // first registered account is an admin
            //var isFirstAccount = _context.Accounts.Count() == 0;
            //account.Role = isFirstAccount ? Role.Admin : Role.User;
            //account.Created = DateTime.UtcNow;
            //account.VerificationToken = randomTokenString();

            // hash password
            var maxId = 0;
            if (_orgUsersRepository.GetQuery().Any())
            {
                maxId = _orgUsersRepository.GetQuery().Max(p => p.Userid);
            }
            account.Userid = maxId + 1;
            account.Password = BC.HashPassword(model.Password);
            account.Enabled = new BitArray(new bool[] { true });
            account.UseWindowsAuth = new BitArray(new bool[] { false });
            account.Orgid = GlobalConstants.DEFAUTL_ORGANIZATION_ID;
            account.Created = DateTime.UtcNow;

            // save account
            await _orgUsersRepository.AddAsync(account);

            // send email
            //sendVerificationEmail(account, origin);
        }

        public AccountResponse GetById(int id)
        {
            var account = _orgUsersRepository.GetQuery().FirstOrDefault(p => p.Userid == id);
            return _mapper.Map<AccountResponse>(account);
        }

        #region private methods
        private string generateJwtToken(OrgUser account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Userid.ToString()) }),
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
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
        #endregion
    }
}
