using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class OrgUser
    {
        public int Userid { get; set; }
        public int Orgid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Loginname { get; set; }
        public BitArray Usewindowsauth { get; set; }
        public string Password { get; set; }
        public BitArray Enabled { get; set; }
        public string Email { get; set; }

        public virtual Organization Org { get; set; }
    }
}
