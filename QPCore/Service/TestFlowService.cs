using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        public TestFlowService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
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
            foreach(var step in value.Steps)
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
    }
}
