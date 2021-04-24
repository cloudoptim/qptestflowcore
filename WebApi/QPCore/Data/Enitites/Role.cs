using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class Role
    {
        public int Roleid { get; set; }
        public string Rolename { get; set; }
        public BitArray Enabled { get; set; }
    }
}
