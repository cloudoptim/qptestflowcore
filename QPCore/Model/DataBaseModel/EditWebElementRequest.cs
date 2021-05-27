using System;
using System.ComponentModel.DataAnnotations;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.DataBaseModel
{
    public class EditWebElementRequest
    {
        [Required]
        public int elementid { get; set; }
        [Required]
        public int pageid { get; set; }
        [Required]
        [IsNotNumber]
        public string elementaliasname { get; set; }
        public string elementtype { get; set; }
        public string itype { get; set; }
        public string ivalue { get; set; }
        public string framenavigation { get; set; }
        public string command { get; set; }
        public string locationpath { get; set; }
        public string screenshot { get; set; }
        public Nullable<int> elementparentid { get; set; }
        public string applicationsection { get; set; }
        public string value { get; set; }
        public Nullable<bool> isactive { get; set; }
        [Required]
        public int groupid { get; set; }
    }
}