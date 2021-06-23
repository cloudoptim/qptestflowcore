using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.CompositeWebElements
{
    public class EditChildCompositeWebElementRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Command { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]

        public int WebELementId { get; set; }

        public override bool Equals(object obj)
        {
            var data = obj as EditChildCompositeWebElementRequest;
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