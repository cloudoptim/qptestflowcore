﻿using Newtonsoft.Json;
using QPCore.DAO;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        public FeatureAppService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
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
            throw new NotImplementedException();
        }
    }
}
