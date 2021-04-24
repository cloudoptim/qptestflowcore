using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QPCore.DAO;
using QPCore.Model.DataBaseModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class CommandService
    {
        PostgresDataBase _postgresDataBase;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postgresDataBase"></param>
        public CommandService(PostgresDataBase postgresDataBase)
        {
            _postgresDataBase = postgresDataBase;
        }

        internal List<Command> getCommands()
        {
            var _data = _postgresDataBase.Procedure("getcommands").ToList().FirstOrDefault();
            List<Command> Commands = JsonConvert.DeserializeObject<List<Command>>(_data.getcommands);
            return Commands;
        }

        internal Command getCommand(int id)
        {
            var _data = _postgresDataBase.Procedure("getcommand", new
            {
                p_commandid = id
            }).ToList().FirstOrDefault();

            if (_data.getcommand is System.DBNull)
                return new Command();

            Command Command = JsonConvert.DeserializeObject<Command>(_data.getcommand);
            return Command;
        }

        internal Command CreateCommand(Command value)
        {
            value.CommandId = 0;
            var json = _postgresDataBase.Procedure("createcommand", 
                                                       new {
                                                           P_CommandId = value.CommandId,
                                                           P_CommandName = value.CommandName,
                                                           P_CommandType  =value.CommandType,
                                                           P_CommandSource = value.CommandSource,
                                                           P_ClientId = value.ClientId,
                                                           P_CommandDescription = value.CommandDescription,
                                                           P_IsActive = value.IsActive
                                                       }).ToList().FirstOrDefault();
            int cmdId = json.createcommand; 
           
            return getCommand(cmdId);
        }

        internal Command UpdateCommand(int id, Command value)
        {
            value.CommandId = id;
            var json = _postgresDataBase.Procedure("createcommand",
                                                       new
                                                       {
                                                           P_CommandId = value.CommandId,
                                                           P_CommandName = value.CommandName,
                                                           P_CommandType = value.CommandType,
                                                           P_CommandSource = value.CommandSource,
                                                           P_ClientId = value.ClientId,
                                                           P_CommandDescription = value.CommandDescription,
                                                           P_IsActive = value.IsActive
                                                       }).ToList().FirstOrDefault();

            int cmdId = json.createcommand;

            return getCommand(cmdId);
        }

        internal void DeleteCommand(int id)
        {
            _postgresDataBase.Procedure("deletecommand", new
            {
                P_CommandId = id
            }).ToList().FirstOrDefault();
        }
    }
}
