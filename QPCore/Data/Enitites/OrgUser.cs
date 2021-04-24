using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class OrgUser
    {
        public OrgUser()
        {
            this.RefreshTokens = new List<RefreshToken>();
        }
        public int Userid { get; set; }
        public int Orgid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public BitArray UseWindowsAuth { get; set; }
        public string Password { get; set; }
        public BitArray Enabled { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public virtual Organization Org { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
    }
}
