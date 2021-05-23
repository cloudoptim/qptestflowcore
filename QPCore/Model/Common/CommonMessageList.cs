using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Common
{
    public class CommonMessageList
    {
        public const string COMMON_ERROR_MESSAGE = "We can not process your request at this time. Please try again later";
        public const string EXISTED_NAME_STRING = "{0} is already in system. Please use the other one.";
        public const string NOT_FOUND_THE_ID = "The Id ({0}) doesn't exist in system.";
        public const string CAN_NOT_DELETE_ITEM = "There are some assiations between it and other objects. Please remove them before you do this action.";
    }
}
