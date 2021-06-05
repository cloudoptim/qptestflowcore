using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WebPages
{
    public class ExistedBulkNameRequest
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public List<string> NameList { get; set; }
    }
}