using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlans
{
    public class TestPlanMessageList
    {
        public const string EXISTED_TEST_PLAN_STRING = "TestPlan Name is already in system. Please use the other one.";
        public const string NOT_FOUND_TEST_PLAN_ID = "The TestPlan Id doesn't exist in system.";
        public const string NOT_FOUND_PARENT_TEST_PLAN_ID = "The Parent TestPlan Id doesn't exist in system.";
        public const string NOT_FOUND_ASSIGN_TO_ID = "The AssignTo Id doesn't exist in system.";
        public const string REQUIRED_TEST_PLAN_NAME = "TestPlan name should be not null or empty string.";
        public const string CAN_NOT_DELETE_TEST_PLAN = "There are some assiations between TestPlan and other objects. Please remove them before you do this action.";
    }
}
