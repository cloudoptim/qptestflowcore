using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Accounts
{
    public class EditAccountRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        public int OrgId { get; set; }
    }
}
