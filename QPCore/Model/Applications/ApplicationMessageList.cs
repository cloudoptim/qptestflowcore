using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Applications
{
    public class ApplicationMessageList
    {
        public const string EXISTED_APPLICATION_STRING = "Application Name is already in system. Please use the other one.";
        public const string NOT_FOUND_APPLICATION_ID = "The Application Id doesn't exist in system.";
        public const string REQUIRED_APPLICATION_NAME = "Application name should be not null or empty string.";
        public const string CAN_NOT_DELETE_APPLICATION = "There are some assiations between Application and other objects. Please remove them before you do this action.";

    }
}
