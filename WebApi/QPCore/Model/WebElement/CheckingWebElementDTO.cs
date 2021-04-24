using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QPCore.Model.WebElement
{
    public class CheckingWebElementDTO
    {
        public CheckingWebElementDTO()
        {
            this.ElementNames = new List<string>();
        }

        [Required]
        public int PageId { get; set; }

        [Required]
        public List<string> ElementNames { get; set; }
    }
}
