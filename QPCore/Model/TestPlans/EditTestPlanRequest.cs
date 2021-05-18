using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlans
{
    public class EditTestPlanRequest : CreateTestPlanRequest, IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ParentId.HasValue && this.ParentId.Value == this.Id)
            {
                yield return new ValidationResult("Parent Id should be different with current Test Plan Id", new[] { nameof(ParentId) });
               
            }
        }
    }
}
