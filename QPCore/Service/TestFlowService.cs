using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using QPCore.Data;
using QPCore.Model.Common;
using QPCore.Model.DataBaseModel.TestFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class TestFlowService
    {
        PostgresDataBase _postgresDataBase;
        private IRepository<QPCore.Data.Enitites.TestFlow> _testFlowRepository;
        private IRepository<QPCore.Data.Enitites.OrgUser> _orgUserRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        /// <param name="testFlowRepository"></param>
        /// <param name="orgUserRepository"></param>
        public TestFlowService(PostgresDataBase postgresDataBase,
            IRepository<QPCore.Data.Enitites.TestFlow> testFlowRepository,
            IRepository<QPCore.Data.Enitites.OrgUser> orgUserRepository)
        {
            _postgresDataBase = postgresDataBase;
            _testFlowRepository = testFlowRepository;
            _orgUserRepository = orgUserRepository;
        }

        [Obsolete("We will change into ")]
        internal List<TestFlow> GetTestFlows()
        {
            var _data = _postgresDataBase.Procedure("gettestflows").ToList().FirstOrDefault();
            List<TestFlow> Commands = JsonConvert.DeserializeObject<List<TestFlow>>(_data.gettestflows);
            return Commands;
        }

        public List<TestFlowItemResponse> GetTestFlowItems()
        {
            var query = _testFlowRepository.GetQuery()
                 .Join(_orgUserRepository.GetQuery(), l => l.LastUpdatedUserId, r => r.UserId, (l, r) => new TestFlowItemResponse()
                 {
                     TestFlowId = l.TestFlowId,
                     TestFlowName = l.TestFlowName,
                     TestFlowDescription = l.TestFlowDescription,
                     LastUpdatedUser = r.FirstName + " " + r.LastName,
                     Islocked = l.Islocked ?? false,
                     IsActive = l.IsActive ?? false,
                     LockedBy = l.LockedBy,
                     TestFlowStatus = l.TestFlowStatus,
                     AssignedDatetTime = l.AssignedDatetTime,
                     AssignedTo = l.AssignedTo,
                     ClientId = l.ClientId,
                     AreaId = l.AreaId,
                     AreaName = l.Area.CategoryName,
                     LastUpdatedUserId = l.LastUpdatedUserId.Value,
                     LastUpdatedDateTime = l.LastUpdatedDateTime.HasValue ? l.LastUpdatedDateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : null,
                     SourceFeatureId = l.SourceFeatureId ?? 1,
                     SourceFeatureName = l.SourceFeatureName
                 })
                 .OrderByDescending(p => p.TestFlowId)
                 .ToList();

            return query;


        }

        internal TestFlow GetTestFlow(int id)
        {
            var _data = _postgresDataBase.Procedure("gettestflow", new
            {
                ptestflowid = id
            }).ToList().FirstOrDefault();

            if (_data.gettestflow is System.DBNull)
                return new TestFlow();

            TestFlow Command = JsonConvert.DeserializeObject<TestFlow>(_data.gettestflow);
            return Command;
        }

        internal TestFlow CreateTestFlow(TestFlow value, int userId)
        {
            value.TestFlowId = 0;
            SetStepColumnRowIdstoZero(value);
            string stepsJson = JsonConvert.SerializeObject(value);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_testflow", stepsJson, NpgsqlDbType.Json));
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("user_id", userId, NpgsqlDbType.Integer));

            var _data = _postgresDataBase.ProcedureJson("create_test_using_json", npgsqlParameters).ToList().FirstOrDefault();
            TestFlow appFeature = JsonConvert.DeserializeObject<TestFlow>(_data.create_test_using_json);

            return appFeature;
        }

        internal TestFlow UpdateTestFlow(int id, TestFlow value, int userId)
        {
            value.TestFlowId = id;
            SetStepColumnRowIdstoZero(value);
            string stepsJson = JsonConvert.SerializeObject(value);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_testflow", stepsJson, NpgsqlDbType.Json));
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("user_id", userId, NpgsqlDbType.Integer));

            var _data = _postgresDataBase.ProcedureJson("create_test_using_json", npgsqlParameters).ToList().FirstOrDefault();
            TestFlow appFeature = JsonConvert.DeserializeObject<TestFlow>(_data.create_test_using_json);

            return appFeature;
        }

        private void SetStepColumnRowIdstoZero(TestFlow value)
        {
            if (value == null)
            {
                value = new TestFlow();
            }

            if (value.Steps == null)
            {
                value.Steps = new List<TestFlowStep>();
            }

            foreach (var step in value.Steps)
            {
                step.TestFlowStepId = 0;
                if (step.Columns == null)
                {
                    step.Columns = new List<TestFlowStepColumn>();
                }
                if (step.Columns != null)
                {
                    foreach (var col in step.Columns)
                    {
                        col.ColumnId = 0;
                        if (col.Rows == null)
                        {
                            col.Rows = new List<TestFlowStepRow>();
                        }
                        foreach (var row in col.Rows)
                        {
                            row.RowId = 0;
                        }
                    }
                }
            }
        }
        internal void DeleteTestFlow(int id)
        {
            _postgresDataBase.Procedure("deletetestflow", new
            {
                ptestflowid = id
            }).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Check unique Test Flow name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="testFlowId"></param>
        /// <returns></returns>
        internal CheckUniqueResponse CheckUniqueTestFlow(string name, int? testFlowId = null)
        {
            var status = _testFlowRepository.GetQuery()
                .Any(
                p => p.TestFlowName.Trim().ToLower() == name.Trim().ToLower() &&
                (!testFlowId.HasValue || p.TestFlowId != testFlowId.Value)
                );
            var result = new CheckUniqueResponse()
            {
                IsUnique = !status
            };

            return result;
        }

        #region Locking Test Flow
        /*
         * A user should able to lock the testflow.
         * a) When userA is locking we needs to update lockedby,Lastupdatedby, lastupdated datetime
         * b) When userA checks for tesflow locking it should be give false.
         * c) When userB check for testflow locking if it is locked by UserA then it should give true.
         * e) UserB  can't unlock the test flow in case UserA is locked Testflow.
         */

        /// <summary>
        /// Lock a test flow
        /// A user should able to lock the testflow.
        /// a) When userA is locking we needs to update lockedby, Lastupdatedby, lastupdated datetime
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal async Task<Data.Enitites.TestFlow> LockTestFlowAsync(int id, int userId)
        {
            Data.Enitites.TestFlow result = null;
            var item = _testFlowRepository.GetQuery()
                .FirstOrDefault(p => p.TestFlowId == id
                    && (p.Islocked == false || (p.Islocked == true && p.LockedBy == userId))
                    );

            if (item != null)
            {
                item.Islocked = true;
                item.LockedBy = userId;
                item.LastUpdatedUserId = userId;
                item.LastUpdatedDateTime = DateTime.UtcNow;
                result = await _testFlowRepository.UpdateAsync(item);
            }

            return result;
        }

        /// <summary>
        /// Lock a test flow
        /// A user should able to lock the testflow.
        /// e) UserB can't unlock the test flow in case UserA is locked Testflow.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal async Task<Data.Enitites.TestFlow> UnlockTestFlowAsync(int id, int userId)
        {
            Data.Enitites.TestFlow result = null;
            var item = _testFlowRepository.GetQuery()
                .FirstOrDefault(p => p.TestFlowId == id &&
                        (p.LockedBy == userId || p.Islocked == false)
                    );

            if (item != null)
            {
                item.Islocked = false;
                item.LockedBy = null;
                item.LastUpdatedUserId = userId;
                item.LastUpdatedDateTime = DateTime.UtcNow;
                result = await _testFlowRepository.UpdateAsync(item);
            }

            return result;
        }

        /// <summary>
        /// Check locking test flow
        /// A user should able to lock the testflow.
        /// b) When userA checks for tesflow locking it should be give false.
        /// c) When userB check for testflow locking if it is locked by UserA then it should give true.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal CheckLockingDTO CheckLockedTestFlow(int id, int userId)
        {
            var result = new CheckLockingDTO()
            {
                IsLocked = false
            };

            var query = _testFlowRepository.GetQuery()
                .Where(p => p.TestFlowId == id && p.Islocked == true && p.LockedBy != userId)
                .Join(_orgUserRepository.GetQuery(), l => l.LockedBy, r => r.UserId, (l, r) => new CheckLockingDTO()
                {
                    IsLocked = l.Islocked.Value,
                    LockedById = r.UserId,
                    LockedByName = $"{r.FirstName} {r.LastName}",
                    LockedByEmail = r.Email,
                    LockedDate = l.LastUpdatedDateTime
                })
                .FirstOrDefault();

            if (query != null)
            {
                result = query;
            }

            return result;
        }

        /// <summary>
        /// Check existing test flow id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool CheckExistedId(int id)
        {
            var query = _testFlowRepository.GetQuery()
                .Any(p => p.TestFlowId == id);

            return query;
        }
        #endregion
    }
}
