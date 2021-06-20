using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.CompositeWebElements
{
    public class CreateChildCompositeWebElementRequest 
    {
        [Required]
        [MaxLength(255)]
        [IsNotNumber]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Command { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]

        public int WebELementId { get; set; }

        public override bool Equals(object obj)
        {
            var data = obj as CreateChildCompositeWebElementRequest;
            if (object.ReferenceEquals(obj, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            var result = this.Command.ToLower().Equals(data.Command.ToLower()) && 
                            this.WebELementId.Equals(data.WebELementId);
            return result;
        }

        public override int GetHashCode()
        {
            int webElementIdHashCode = this.WebELementId.GetHashCode();
            int commandHashCode = this.Command == null ? 0 : this.Command.GetHashCode();
            return webElementIdHashCode ^ commandHashCode;
        }
    }
}