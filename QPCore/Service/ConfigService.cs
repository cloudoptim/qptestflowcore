using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using QPCore.Model.DataBaseModel.Commands;
using QPCore.Model.DataBaseModel.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class ConfigService
    {
        PostgresDataBase _postgresDataBase;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        public ConfigService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
        }

        internal List<TestConfigViewModel> getConfigs()
        {
            var _data = _postgresDataBase.Procedure("getconfigs").ToList().FirstOrDefault();

            if (_data.getconfigs is DBNull)
               return  new List<TestConfigViewModel>();

                List<TestConfigViewModel> configs = JsonConvert.DeserializeObject<List<TestConfigViewModel>>(_data.getconfigs);
            return configs;
        }

        internal TestConfigViewModel getConfig(int id)
        {
            var _data = _postgresDataBase.Procedure("getconfig", new
            {
                P_ConfigId = id
            }).ToList().FirstOrDefault();

            if (_data.getconfig is System.DBNull)
                return new TestConfigViewModel();

            TestConfigViewModel Command = JsonConvert.DeserializeObject<TestConfigViewModel>(_data.getconfig);
            return Command;
        }

        internal TestConfigKeys getConfigKey(int id)
        {
            var json = _postgresDataBase.Procedure("getconfigkey",
                                                        new
                                                        {
                                                            P_PairId = id
                                                        }).ToList().FirstOrDefault();

            if (json.getconfigkey is DBNull)
            {
                return new TestConfigKeys();
            }
            TestConfigKeys configkeys = JsonConvert.DeserializeObject<TestConfigKeys>(json.getconfigkey);
            return configkeys;
        }

        internal TestConfigKeys CreateConfigKey(TestConfigKeys value)
        {
            value.PairId = 0;
            var json = _postgresDataBase.Procedure("createtestflowconfigkeys",
                                                       new
                                                       {
                                                           P_PairId = value.PairId,
                                                           P_ConfigId = value.ConfigId,
                                                           P_KeyName = value.KeyName,
                                                           P_KeyValue = value.KeyValue

                                                       }).ToList().FirstOrDefault();

            if(json.createtestflowconfigkeys is DBNull)
            {
                return new TestConfigKeys();
            }
            TestConfigKeys configkeys = JsonConvert.DeserializeObject<TestConfigKeys>(json.createtestflowconfigkeys);
            return configkeys;
        }

        internal TestConfigKeys UpdateConfigKeys(int id, TestConfigKeys value)
        {
            value.PairId = id;
            var json = _postgresDataBase.Procedure("createtestflowconfigkeys",
                                                       new
                                                       {
                                                           P_PairId = value.PairId,
                                                           P_ConfigId = value.ConfigId,
                                                           P_KeyName = value.KeyName,
                                                           P_KeyValue = value.KeyValue

                                                       }).ToList().FirstOrDefault();

            if (json.createtestflowconfigkeys is DBNull)
            {
                return new TestConfigKeys();
            }
            TestConfigKeys configkeys = JsonConvert.DeserializeObject<TestConfigKeys>(json.createtestflowconfigkeys);
            return configkeys;
        }

        internal TestConfig CreateConfig(TestConfig value)
        {
            value.ConfigId = 0;
            var json = _postgresDataBase.Procedure("createtestflowconfig", 
                                                       new {
                                                           P_ConfigId = value.ConfigId,
                                                           P_ClientId = value.ClientId,
                                                           P_ConfigName = value.ConfigName,
                                                           P_IsActive = value.IsActive,
                                                           P_IsSystemDefined = value.IsSystemDefined,
                                                          
                                                       }).ToList().FirstOrDefault();

            TestConfig config = JsonConvert.DeserializeObject<TestConfig>(json.createtestflowconfig);
            return config;
        }

        internal TestConfig UpdateConfig(int id, TestConfig value)
        {
            value.ConfigId = id;
            var json = _postgresDataBase.Procedure("createtestflowconfig",
                                                       new
                                                       {
                                                           P_ConfigId = value.ConfigId,
                                                           P_ClientId = value.ClientId,
                                                           P_ConfigName = value.ConfigName,
                                                           P_IsActive = value.IsActive,
                                                           P_IsSystemDefined = value.IsSystemDefined,

                                                       }).ToList().FirstOrDefault();

            TestConfig config = JsonConvert.DeserializeObject<TestConfig>(json.createtestflowconfig);
            return config;
        }

        internal bool DeleteConfig(int id)
        {
            _postgresDataBase.Procedure("deleteconfig", new
            {
                P_ConfigId = id
            }).ToList().FirstOrDefault();
            return true;
        }

        internal bool DeleteConfigKey(int id)
        {
            _postgresDataBase.Procedure("deleteconfigkey", new
            {
                P_keyid = id
            }).ToList().FirstOrDefault();
            return true;
        }
    }
}
