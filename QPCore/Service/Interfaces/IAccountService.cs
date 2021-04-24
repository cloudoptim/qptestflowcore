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

        Task Register(RegisterRequest model, string origin);

        AccountResponse GetById(int id);
    }
}
