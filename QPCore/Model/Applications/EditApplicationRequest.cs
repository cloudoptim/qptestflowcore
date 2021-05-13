using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Applications
{
    public class EditApplicationRequest
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ApplicationName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int OrgId { get; set; }
    }
}
