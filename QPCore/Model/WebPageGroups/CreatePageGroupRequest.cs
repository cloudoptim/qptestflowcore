using System.ComponentModel.DataAnnotations;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.WebPageGroups
{
    public class CreatePageGroupRequest
    {
        [Required]
        [MaxLength(250)]
        [IsNotNumber]
        public string Name { get; set; }
        public int? VersionId { get; set; }
    }
}