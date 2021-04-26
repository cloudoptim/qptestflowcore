using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class AppUser
    {
        public int Userclientid { get; set; }
        public int Userid { get; set; }
        public int Client { get; set; }
        public BitArray Enabled { get; set; }

        public virtual Application Application { get; set; }
        public virtual OrgUser OrgUser { get; set; }
    }
}
