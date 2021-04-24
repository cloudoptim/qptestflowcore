using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebCommand
    {
        public int CommandId { get; set; }
        public string CommandName { get; set; }
        public string CommandType { get; set; }
        public string CommandSource { get; set; }
        public int ClientId { get; set; }
        public string CommandDescription { get; set; }
        public bool? IsActive { get; set; }

        public virtual Application Client { get; set; }
    }
}
