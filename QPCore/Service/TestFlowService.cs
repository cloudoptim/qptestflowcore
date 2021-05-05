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
        public TestFlowService(PostgresDataBase postgresDataBase,
            IRepository<QPCore.Data.Enitites.TestFlow> testFlowRepository,
            IRepository<QPCore.Data.Enitites.OrgUser> orgUserRepository)
        {
            _postgresDataBase = postgresDataBase;
            _testFlowRepository = testFlowRepository;
            _orgUserRepository = orgUserRepository;
        }

        internal List<TestFlow> GetTestFlows()
        {
            var _data = _postgresDataBase.Procedure("gettestflows").ToList().FirstOrDefault();
            List<TestFlow> Commands = JsonConvert.DeserializeObject<List<TestFlow>>(_data.gettestflows);
            return Commands;
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

        internal TestFlow CreateTestFlow(TestFlow value)
        {
            value.TestFlowId = 0;
            SetStepColumnRowIdstoZero(value);
            string stepsJson = JsonConvert.SerializeObject(value);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_testflow", stepsJson, NpgsqlDbType.Json));

            var _data = _postgresDataBase.ProcedureJson("createtestusingjson", npgsqlParameters).ToList().FirstOrDefault();
            TestFlow appFeature = JsonConvert.DeserializeObject<TestFlow>(_data.createtestusingjson);

            return appFeature;
        }

        internal TestFlow UpdateTestFlow(int id, TestFlow value)
        {
            value.TestFlowId = id;
            SetStepColumnRowIdstoZero(value);
            string stepsJson = JsonConvert.SerializeObject(value);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_testflow", stepsJson, NpgsqlDbType.Json));

            var _data = _postgresDataBase.ProcedureJson("createtestusingjson", npgsqlParameters).ToList().FirstOrDefault();
            TestFlow appFeature = JsonConvert.DeserializeObject<TestFlow>(_data.createtestusingjson);

            return appFeature;
        }

        private void SetStepColumnRowIdstoZero(TestFlow value)
        {
            foreach (var step in value.Steps)
            {
                step.TestFlowStepId = 0;
                if (step.Columns != null)
                {
                    foreach (var col in step.Columns)
                    {
                        col.ColumnId = 0;
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
        /// <returns></returns>
        internal CheckUniqueDTO CheckUniqueTestFlow(string name)
        {
            var status = _testFlowRepository.GetQuery()
               .Any(p => p.TestFlowName.Trim().ToLower() == name.Trim().ToLower());
            var result = new CheckUniqueDTO()
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
                .FirstOrDefault(p => p.TestFlowId == id && p.Islocked == false);

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
                .FirstOrDefault(p => p.TestFlowId == id && p.LockedBy == userId);

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
        #endregion
    }
}
