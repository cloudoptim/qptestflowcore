using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Accounts
{
    public class ValidateRefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
