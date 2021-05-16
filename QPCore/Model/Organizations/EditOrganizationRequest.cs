using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Organizations
{
    public class EditOrganizationRequest : CreateOrganizationRequest
    {
        [Required]
        public int OrgId { get; set; }
    }
}
