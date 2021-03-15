using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using QPCore.Model.DataBaseModel.Commands;
using QPCore.Model.DataBaseModel.Configurations;
using QPCore.Model.TestRun;
using QPTestClient.QPFlow.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class TestRunService
    {
        PostgresDataBase _postgresDataBase;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        public TestRunService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
        }


        internal int Createbatch(Batch value)
        {
            value.RunBatchId = 0;
            var json = _postgresDataBase.Procedure("createbatch",
                                                       new
                                                       {
                                                           P_RunBatchId = value.RunBatchId,
                                                           P_ClientId = value.ClientId,
                                                           P_BatchStartDate = value.BatchStartDate,
                                                           P_BatchEndDate = value.BatchEndDate,
                                                           P_RunBy = value.RunBy,
                                                           P_BatchOutCome = value.BatchOutcome
                                                       
                                                       }).ToList().FirstOrDefault();
            int batchId = json.createbatch;

            return batchId;
        }
        internal int Updatebatch(Batch value,int id)
        {
            value.RunBatchId = id;
            var json = _postgresDataBase.Procedure("createbatch",
                                                       new
                                                       {
                                                           P_RunBatchId = value.RunBatchId,
                                                           P_ClientId = value.ClientId,
                                                           P_BatchStartDate = value.BatchStartDate,
                                                           P_BatchEndDate = value.BatchEndDate,
                                                           P_RunBy = value.RunBy,
                                                           P_BatchOutCome = value.BatchOutcome

                                                       }).ToList().FirstOrDefault();
            int batchId = json.createbatch;

            return batchId;
        }

        internal TestRunResult getTestResult(int id)
        {
            var _data = _postgresDataBase.Procedure("getlocalrun", new
            {
                p_resultid = id
            }).ToList().FirstOrDefault();
            TestRunResult testRunResult = JsonConvert.DeserializeObject<TestRunResult>(_data.getlocalrun);
            return testRunResult;
        }
        internal int Createrun(Run value)
        {
            value.RunId = 0;
            var json = _postgresDataBase.Procedure("createrun",
                                                       new
                                                       {
                                                           P_RunId = value.RunId,
                                                           P_Batchid = value.Batchid,
                                                           P_ApplicationName = value.ApplicationName,
                                                           P_RunStatus = value.RunStatus,
                                                           P_RunStartDate = value.RunStartDate,
                                                           P_RunEndDate = value.RunEndDate,
                                                           P_RuntStdouput = value.RuntStdouput

                                                       }).ToList().FirstOrDefault();
            int batchId = json.createrun;

            return batchId;
        }
        internal int Updaterun(Run value , int id)
        {
            value.RunId = id;
            var json = _postgresDataBase.Procedure("createrun",
                                                       new
                                                       {
                                                           P_RunId = value.RunId,
                                                           P_Batchid = value.Batchid,
                                                           P_ApplicationName = value.ApplicationName,
                                                           P_RunStatus = value.RunStatus,
                                                           P_RunStartDate = value.RunStartDate,
                                                           P_RunEndDate = value.RunEndDate,
                                                           P_RuntStdouput = value.RuntStdouput

                                                       }).ToList().FirstOrDefault();
            int batchId = json.createrun;

            return batchId;
        }
        internal int Createtestcase(TestCase value)
        {
            value.RunTestCaseId = 0;
            var json = _postgresDataBase.Procedure("createruntestcase",
                                                       new
                                                       {
                                                           P_RunTestCaseId = value.RunTestCaseId,
                                                           P_TestcaseName = value.TestcaseName,
                                                           P_TestCaseRunStatus = value.TestCaseRunStatus,
                                                           P_TestCaseRunStartDate = value.TestCaseRunStartDate,
                                                           P_TestCaseRunEndDate = value.TestCaseRunEndDate,
                                                           P_TestCaseRunErrorMessage = value.TestCaseRunErrorMessage,
                                                           P_TestRunId = value.TestRunId,
                                                           P_configId = value.configId,
                                                            P_TestCaseId = value.TestCaseId
                                                       }).ToList().FirstOrDefault();
            int batchId = json.createruntestcase;

            return batchId;
        }
        internal int Updatetest(TestCase value, int id)
        {
            value.RunTestCaseId = id;
            var json = _postgresDataBase.Procedure("createruntestcase",
                                                       new
                                                       {
                                                           P_RunTestCaseId = value.RunTestCaseId,
                                                           P_TestcaseName = value.TestcaseName,
                                                           P_TestCaseRunStatus = value.TestCaseRunStatus,
                                                           P_TestCaseRunStartDate = value.TestCaseRunStartDate,
                                                           P_TestCaseRunEndDate = value.TestCaseRunEndDate,
                                                           P_TestCaseRunErrorMessage = value.TestCaseRunErrorMessage,
                                                           P_TestRunId = value.TestRunId,
                                                           P_configId = value.configId,
                                                           P_TestCaseId = value.TestCaseId

                                                       }).ToList().FirstOrDefault();
            int batchId = json.createruntestcase;

            return batchId;
        }
        internal int Createteststep(TestStep value)
        {
            value.RunStepId = 0;
            SetStepColumnRowIdstoZero(value);
            string stepsJson = JsonConvert.SerializeObject(value);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_steps", stepsJson, NpgsqlDbType.Json));

            var _data = _postgresDataBase.ProcedureJson("createrunstepsusingjson", npgsqlParameters).ToList().FirstOrDefault();

            return _data.createrunstepsusingjson;
        }
        private void SetStepColumnRowIdstoZero(TestStep steps)
        {
            foreach (var col in steps.Columns)
            {
                col.ColumnId = 0;
                foreach (var row in col.Rows)
                {
                    row.RowId = 0;
                }
            }

        }
        internal int Addresults(TestRunResult testresult)
        {
            var testResultJson  = JsonConvert.SerializeObject(testresult);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("p_jresult", testResultJson, NpgsqlDbType.Json));
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("p_clientid", testresult.clientId, NpgsqlDbType.Integer));
            var json = _postgresDataBase.ProcedureJson("createlocalresult", npgsqlParameters).ToList().FirstOrDefault();
            var newwebModel = json.createlocalresult;
            return newwebModel;
        }
    }
}
