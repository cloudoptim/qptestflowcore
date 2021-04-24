using AutomationAssistant.Models.AppConfig;
using DataBaseModel;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WebModelService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        PostgresDataBase _postgresDataBase;
        public WebModelService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
        }

        public List<WebModel> GetWebModels()
        {
            var _data = _postgresDataBase.Procedure<WebModel>("getmodels").ToList();
            return _data;
        }
        public WebModel GetWebModel(int id)
        {
            var json = _postgresDataBase.Procedure("getmodeljson", new { modelid = id }).ToList().FirstOrDefault();
            WebModel webModel = JsonConvert.DeserializeObject<WebModel>(json.getmodeljson);

            return webModel;
        }
        //todo: refector to move sql specific code into DAO
        public WebModel AddModel(WebModel webModel)
        {
            string webmodelJson = JsonConvert.SerializeObject(webModel);
            List<NpgsqlParameter> npgsqlParameters = new List<NpgsqlParameter>();
            npgsqlParameters.Add(_postgresDataBase.CreateParameter("j_model", webmodelJson, NpgsqlDbType.Json));
            var json = _postgresDataBase.ProcedureJson("createmodelusingjson", npgsqlParameters).ToList().FirstOrDefault();
            WebModel newwebModel = JsonConvert.DeserializeObject<WebModel>(json.createmodelusingjson);

            return newwebModel;
        }

        public void deleteModel(int id)
        {
             _postgresDataBase.Procedure("deletemodel", new { modelid = id }).ToList().FirstOrDefault();
        }
    }
}
