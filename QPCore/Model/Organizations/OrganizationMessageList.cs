using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Organizations
{
    public class OrganizationMessageList
    {
        public const string EXISTED_ORGANIZATION_STRING = "Organization Name is already in system. Please use the other one.";
        public const string NOT_FOUND_ORGANIZATION_ID = "The Organization Id doesn't exist in system.";
        public const string REQUIRED_ORGANIZATION_NAME = "Organization name should be not null or empty string.";
        public const string CAN_NOT_DELETE_ORGANIZATION = "There are some assiations between Organization and Applications or User. Please remove them before you do this action.";
    }
}
