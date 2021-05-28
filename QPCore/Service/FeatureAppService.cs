using Newtonsoft.Json;
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
    public class FeatureAppService
    {
        PostgresDataBase _postgresDataBase;
        private readonly IRepository<ApplicationFeature> _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        /// <param name="repository"></param>
        public FeatureAppService(PostgresDataBase postgresDataBase,
            IRepository<ApplicationFeature> repository)
        {
            _postgresDataBase = postgresDataBase;
            _repository = repository;
        }

        public List<AppFeature>  GetAppFeature()
        {
            var _data = _postgresDataBase.Procedure("getapplicationfeatures").ToList().FirstOrDefault();
            List<AppFeature> appfeatures = JsonConvert.DeserializeObject<List<AppFeature>>(_data.getapplicationfeatures);
            return appfeatures;
        }

        internal AppFeatureView CreateFeature(AppFeatureView feature)
        {

            var _data = _postgresDataBase.Procedure("createapplicationfeature",new { 
                AppFeatureId =0,
                FeatureName = feature.FeatureName,
                ParentFeatureId = feature.ParentFeatureId,
                IsActive = feature.IsActive,
                ClientId = feature.ClientId
            }).ToList().FirstOrDefault();
            AppFeatureView appFeature = JsonConvert.DeserializeObject<AppFeatureView>(_data.createapplicationfeature);

            return appFeature;
        }

        internal List<Steps> GetAppFeatureSteps(int id)
        {
            var _data = _postgresDataBase.Procedure("getsteps", new
            {   
                featureid = id
            }).ToList().FirstOrDefault();

            if (_data.getsteps is System.DBNull)
                return new List<Steps>();

            List<Steps> stepList = JsonConvert.DeserializeObject<List<Steps>>(_data.getsteps);
            return stepList;
        }

        internal AppFeatureView UpdateFeature(AppFeatureView feature)
        {
            var _data = _postgresDataBase.Procedure("createapplicationfeature", new
            {
                AppFeatureId = feature.AppFeatureId,
                FeatureName = feature.FeatureName,
                ParentFeatureId = feature.ParentFeatureId,
                IsActive = feature.IsActive,
                ClientId = feature.ClientId
            }).ToList().FirstOrDefault();
            AppFeatureView appFeature = JsonConvert.DeserializeObject<AppFeatureView>(_data.createapplicationfeature);

            return appFeature;
        }

        internal void Delete(int id)
        {
            throw new NotImplementedException();
        }

        internal void DeleteFeature(int id)
        {
            _postgresDataBase.Procedure("deletefeature", new { pfeatureid = id }).ToList().FirstOrDefault();
        }

        internal bool CheckFeatureNameExisted(string name, int? parentAppFeatureId = null, int? appFeatureId = null)
        {
            name = name.Trim().ToLower();
            var isExisted = _repository.GetQuery()
                        .Any(p => p.FeatureName.ToLower() == name
                                && (p.ParentFeatureId == parentAppFeatureId)
                                && (!appFeatureId.HasValue || (appFeatureId.HasValue && p.AppFeatureId != appFeatureId.Value)));

            return isExisted;
        }

        internal bool CheckCanDelete(int appFeatureId)
        {
            var query = _repository.GetQuery()
                .Any(p => p.AppFeatureId == appFeatureId &&
                        (   p.StepGlossaries.Any(s => s.StepSource.ToLower().Trim() == "code") ||
                            p.Childs.Any(c => c.StepGlossaries.Any(s => s.StepSource.ToLower().Trim() == "code"))
                        )
                    );

            return !query;
        }

        internal bool CheckExistedId(int id)
        {
            var isExisted = _repository.GetQuery()
                    .Any(p => p.AppFeatureId == id);

            return isExisted;
        }
    }
}
