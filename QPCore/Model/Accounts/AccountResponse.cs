using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Accounts
{
    public class AccountResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
