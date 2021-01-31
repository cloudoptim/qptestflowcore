using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.ViewModels
{
    public class WebPageGroupViewModel
    {

        public int id { get; set; }
        public string groupname { get; set; }
        public string createdby { get; set; }
        public Nullable<System.DateTime> createddatetime { get; set; }
        public string updatedBy { get; set; }
        public Nullable<System.DateTime> updateddatetime { get; set; }
        public Nullable<bool> isactive { get; set; }
        public Nullable<int> versionid { get; set; }
    }

    public class CreateWebPageGroupViewModel
    {

     
        public string groupname { get; set; }
        public string createdby { get; set; }
        public Nullable<int> versionid { get; set; }
    }


    public class UpdateWebPageGroupViewModel:CreateWebPageGroupViewModel
    {

        public int id { get; set; }

    }
}
