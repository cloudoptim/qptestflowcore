using System.ComponentModel.DataAnnotations;
using QPCore.Model.Common;

namespace QPCore.Model.WebPages
{
    public class WebPageExistedNameRequest : ExistedNameRequest
    {
        [Required]
        public int GroupId { get; set; }
    }
}