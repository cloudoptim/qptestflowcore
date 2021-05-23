using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class StepService
    {
        private PostgresDataBase _postgresDataBase;
        private IRepository<StepGlossary> _stepGlossaryRepository;
        private IRepository<TestFlowStep> _testFlowStep;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        /// <param name="stepGlossaryRepository"></param>
        /// <param name="testFlowStep"></param>
        public StepService(PostgresDataBase postgresDataBase,
            IRepository<StepGlossary> stepGlossaryRepository,
            IRepository<TestFlowStep> testFlowStep)
        {
            _postgresDataBase = postgresDataBase;
            _stepGlossaryRepository = stepGlossaryRepository;
            _testFlowStep = testFlowStep;
        }

        internal Steps CreateStep(Steps steps)
        {
            steps.StepId = 0;
            SetStepColumnRowIdstoZero(steps);

            string stepsJson = JsonConvert.SerializeObject(steps);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_steps", stepsJson, NpgsqlDbType.Json));

            var _data = _postgresDataBase.ProcedureJson("createstepsusingjson", npgsqlParameters).ToList().FirstOrDefault();
            Steps appFeature = JsonConvert.DeserializeObject<Steps>(_data.createstepsusingjson);

            return appFeature;
        }

        internal Steps Getstep(int id)
        {
            var _data = _postgresDataBase.Procedure("getstep", new
            {
                pstepid = id
            }).ToList().FirstOrDefault();

            if (_data.getstep is System.DBNull)
                return new Steps();

            Steps stepList = JsonConvert.DeserializeObject<Steps>(_data.getstep);
            return stepList;
        }


        internal Steps UpdateStep(Steps steps)
        {


            SetStepColumnRowIdstoZero(steps);

            string stepsJson = JsonConvert.SerializeObject(steps);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_steps", stepsJson, NpgsqlDbType.Json));

            var _data = _postgresDataBase.ProcedureJson("createstepsusingjson", npgsqlParameters).ToList().FirstOrDefault();
            Steps appFeature = JsonConvert.DeserializeObject<Steps>(_data.createstepsusingjson);

            return appFeature;
        }

        private void SetStepColumnRowIdstoZero(Steps steps)
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
        internal void DeleteStep(int id)
        {
            _postgresDataBase.Procedure("deletestep", new { pstepid = id }).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Checking unique step
        /// </summary>
        /// <param name="featureId"></param>
        /// <param name="stepGlossaryName"></param>
        /// <returns></returns>
        [Obsolete("We will replace it by CheckUniqueStepInFeature")]
        internal CheckUniqueResponse CheckUniqueStepGlossary(int featureId, string stepGlossaryName)
        {
            var status = _stepGlossaryRepository.GetQuery()
                .Any(p => p.FeatureId == featureId
                && p.StepName.Trim().ToLower() == stepGlossaryName.Trim().ToLower());
            var result = new CheckUniqueResponse()
            {
                IsUnique = status
            };

            return result;
        }

        internal CheckUniqueResponse CheckUniqueStepInFeature(int featureId, string stepGlossaryName, int? stepId = null)
        {
            var status = _stepGlossaryRepository.GetQuery()
                .Any(p => p.FeatureId == featureId
                && p.StepName.Trim().ToLower() == stepGlossaryName.Trim().ToLower()
                && (!stepId.HasValue || p.StepId != stepId.Value)
                );
            var result = new CheckUniqueResponse()
            {
                IsUnique = !status
            };

            return result;
        }

        /// <summary>
        /// Check step is used by step flow
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool CheckIsUsed(int id)
        {
            var query = _testFlowStep.GetQuery()
                 .Any(p => p.StepGlossaryStepId == id);

            return query;
        }
    }
}
