using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.Commands
{
    public class Command
    {
        public int CommandId { get; set; }
        public string CommandName { get; set; }
        public string CommandType { get; set; }
        public string CommandSource { get; set; }
        public int ClientId { get; set; }
        public string CommandDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
