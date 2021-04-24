using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class UserRole
    {
        public int Clientroleassoc { get; set; }
        public int Userclientid { get; set; }
        public int Roleid { get; set; }
        public BitArray Enabled { get; set; }
    }
}
