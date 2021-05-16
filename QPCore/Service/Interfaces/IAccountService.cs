using QPCore.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, string ipAddress);

        Task RegisterAsync(RegisterRequest model, string origin);

        AccountResponse GetById(int id);

        Task VerifyEmailAsync(string token);

        Task ForgotPasswordAsync(ForgotPasswordRequest model, string origin);

        bool ValidateResetToken(ValidateResetTokenRequest model);

        Task ResetPasswordAsync(ResetPasswordRequest model);

        Task<AuthenticateResponse> RefreshTokenAsync(string token, string ipAddress);

        Task RevokeTokenAsync(string token, string ipAddress);

        bool OwnsToken(int userId, string token);

        /// <summary>
        /// Check existed user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CheckExistedId(int userId);

        /// <summary>
        /// Check existing email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CheckExistedEmail(string email, int? userId = null);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        List<AccountResponse> GetAll();

        /// <summary>
        /// Get all accounts in organization
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<AccountResponse> GetByOrgId(int orgId);

        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="editAccountRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<AccountResponse> UpdateAsync(EditAccountRequest editAccountRequest, int userId);
    }
}
