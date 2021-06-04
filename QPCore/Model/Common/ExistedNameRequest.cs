using System.ComponentModel.DataAnnotations;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.Common
{
    public class ExistedNameRequest
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(250)]
        [IsNotNumber]
        public string Name { get; set; }
    }
}