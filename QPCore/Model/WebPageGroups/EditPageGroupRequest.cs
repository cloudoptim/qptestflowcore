using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WebPageGroups
{
    public class EditPageGroupRequest : CreatePageGroupRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}