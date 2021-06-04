using System.ComponentModel.DataAnnotations;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.WebPages
{
    public class CreateWebPageRequest
    {
        [Required]
        [MaxLength(250)]
        [IsNotNumber]
        public string Name { get; set; }

        [Required]
        public int GroupId { get; set; }
    }
}