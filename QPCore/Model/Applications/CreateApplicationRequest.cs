using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Applications
{
    public class CreateApplicationRequest
    {
        [Required]
        [MaxLength(255)]
        public string ApplicationName { get; set; }

        [Required]
        public int OrgId { get; set; }
    }
}
