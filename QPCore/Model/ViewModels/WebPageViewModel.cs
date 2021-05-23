using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.ViewModels
{/// <summary>
/// 
/// </summary>
    public class WebPageViewModel
    {
        public int pageId { get; set; }
        public int groupId { get; set; }
        [Required]
        [IsNotNumber]
        public string pageName { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> createdDateTime { get; set; }
        public string updateddBy { get; set; }
        public Nullable<System.DateTime> updatedDateTime { get; set; }
        public Nullable<bool> isActive { get; set; }
    }

    
    public class UpdatePageViewModel:CreatePageViewModel
    {
        [Required]
        public int pageId { get; set; }
    }
    public class CreatePageViewModel
    {
        public int groupId { get; set; }
        [Required]
        [IsNotNumber]
        public string pageName { get; set; }
        public string createdBy { get; set; }
    }
}