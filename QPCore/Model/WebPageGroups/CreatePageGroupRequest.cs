using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WebPageGroups
{
    public class CreatePageGroupRequest
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public int? VersionId { get; set; }
    }
}