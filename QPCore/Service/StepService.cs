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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        /// <param name="stepGlossaryRepository"></param>
        public StepService(PostgresDataBase postgresDataBase,
            IRepository<StepGlossary> stepGlossaryRepository)
        {
            _postgresDataBase = postgresDataBase;
            _stepGlossaryRepository = stepGlossaryRepository;
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
        internal CheckUniqueDTO CheckUniqueStepGlossary(int featureId, string stepGlossaryName)
        {
            var status = _stepGlossaryRepository.GetQuery()
                .Any(p => p.FeatureId == featureId
                && p.StepName.Trim().ToLower() == stepGlossaryName.Trim().ToLower());
            var result = new CheckUniqueDTO()
            {
                IsUnique = status
            };

            return result;
        }
    }
}
