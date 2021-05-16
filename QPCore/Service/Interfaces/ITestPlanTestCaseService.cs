using QPCore.Model.TestPlanTestCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface ITestPlanTestCaseService
    {
        /// <summary>
        /// Get all TestPlan TestCase
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        List<TestPlanTestCaseResponse> GetAll(int testPlanId);

        /// <summary>
        /// Create new testplan testcase
        /// </summary>
        /// <param name="createTestPlanTestCaseRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<TestPlanTestCaseResponse>> CreateAsync(CreateTestPlanTestCaseRequest createTestPlanTestCaseRequest, int userId);

        /// <summary>
        /// Edit testplan testcase
        /// </summary>
        /// <param name="editTestPlanTestCaseRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TestPlanTestCaseResponse> UpdateAsync̣̣(EditTestPlanTestCaseRequest editTestPlanTestCaseRequest, int userId);

        /// <summary>
        /// Delete testplan testcase by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Check existed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExistedId(int id);

        /// <summary>
        /// Check existed assignment
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <param name="testFlowId"></param>
        /// <returns></returns>
        bool CheckExistedAssignment(int testPlanId, int testFlowId);
    }
}
