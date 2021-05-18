using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Common.Validations
{
    public class IsNotNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueString = value.ToString();

            if(valueString.All(Char.IsDigit))
            {
                return new ValidationResult($"{validationContext.MemberName} should not be a number.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
