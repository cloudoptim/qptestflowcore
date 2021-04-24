using AutomationAssistant.Models.AppConfig;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QPCore.DAO
{
    public class PostgresDataBase
    {
        string _connectionString;
        public PostgresDataBase(IAADatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public dynamic Single(string query, params object[] parameters)
        {
            return CoreFunction(ReturnSingle, (q, p) =>
            {
                q = q.Replace("{if(#CK_H1#==1){return 1;}}", "{{if(#CK_H1#==1){{return 1;}}}}");
                return string.Format(q, p);
            }, query, parameters);
        }

        public TClass Single<TClass>(string query, params object[] parameters) where TClass : class, new()
        {
            var result = CoreFunction(ReturnSingle, (q, p) =>
            {
                q = q.Replace("{if(#CK_H1#==1){return 1;}}", "{{if(#CK_H1#==1){{return 1;}}}}");
                return string.Format(q, p);
            }, query, parameters);

            var properties = typeof(TClass).GetProperties();

            var typedResult = new TClass();

            foreach (var property in properties)
            {
                foreach (var dynamicProperty in ((IDictionary<string, object>)result).Keys)
                {
                    if (dynamicProperty.ToLower() == property.Name.ToLower())
                    {
                        var value = ((IDictionary<string, object>)result).Single(v => v.Key == dynamicProperty).Value;

                        var typedPropertyResult = GetTypedProperty<TClass>(value, property);

                        property.SetValue(typedResult, typedPropertyResult, null);

                        break;
                    }
                }
            }

            return typedResult;
        }

        public IEnumerable<dynamic> Multiple(string query, params object[] parameters)
        {
            return CoreFunction(ReturnMultiple, (q, p) =>
            {
               
                q = q.Replace("{if(#CK_H1#==1){return 1;}}", "{{if(#CK_H1#==1){{return 1;}}}}");
                return string.Format(q, p);
            }, query, parameters);
        }

        public object Scalar(string query, params object[] parameters)
        {
            return CoreFunction(ReturnScalar, (q, p) =>
            {
             
                q = q.Replace("{if(#CK_H1#==1){return 1;}}", "{{if(#CK_H1#==1){{return 1;}}}}");
                return string.Format(q, p);
            }, query, parameters);
        }

        public int NonQuery(string query, params object[] parameters)
        {
            return CoreFunction(ReturnNonQuery, (q, p) =>
            {
              
                q = q.Replace("{if(#CK_H1#==1){return 1;}}", "{{if(#CK_H1#==1){{return 1;}}}}");

                return p == null || p.Length == 0 ? q : string.Format(q, p);

            }, query, parameters);
        }

        public IEnumerable<TClass> Multiple<TClass>(string query, params object[] parameters) where TClass : class, new()
        {
            var results = Multiple(query, parameters);

            var typedResults = ConvertToTypedSet<TClass>(results);

            return typedResults;
        }
        public IEnumerable<TClass> Procedure<TClass>(string procedureName) where TClass : class, new()
        {
            var results = Procedure(procedureName, null);

            var typedResults = ConvertToTypedSet<TClass>(results);

            return typedResults;
        }

        public IEnumerable<dynamic> Procedure(string procedureName)
        {
            return Procedure(procedureName, null);
        }

        public IEnumerable<TClass> Procedure<TClass>(string procedureName, object parameters) where TClass : class, new()
        {
            var result = Procedure(procedureName, parameters);

            var typedResults = ConvertToTypedSet<TClass>(result);

            return typedResults;
        }

        public IEnumerable<dynamic> Procedure(string procedureName, object parameters)
        {
            if (parameters != null)
            {
                var sb = new StringBuilder();
                var properties = parameters.GetType().GetProperties();
                foreach (var property in properties)
                {
                    sb.AppendLine(String.Format("@{0} = '{1}'", property.Name, property.GetValue(parameters, null)));
                }
             
            }
            else
            {
                
            }
            try
            {
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();

                    using (var procedure = new NpgsqlCommand(procedureName, sqlConnection))
                    {
                        procedure.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            var properties = parameters.GetType().GetProperties();

                            foreach (var property in properties)
                            {
                                var sqlParameter = new NpgsqlParameter(property.Name, property.GetValue(parameters, null));

                                procedure.Parameters.Add(sqlParameter);
                            }
                        }

                        using (NpgsqlDataReader procedureResult = procedure.ExecuteReader())
                        {
                            var resultSet = ReadFromSqlDataReader(procedureResult);

                            return resultSet;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
             
                throw ex;
            }
        }

        public IEnumerable<dynamic> ProcedureJson(string procedureName, List<NpgsqlParameter> npgsqlParameters)
        {
         
            try
            {
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();

                    using (var procedure = new NpgsqlCommand(procedureName, sqlConnection))
                    {
                        procedure.CommandType = CommandType.StoredProcedure;

                       

                            foreach (var sqlParameter in npgsqlParameters)
                            {
                               procedure.Parameters.Add(sqlParameter);
                            }
                        

                        using (NpgsqlDataReader procedureResult = procedure.ExecuteReader())
                        {
                            var resultSet = ReadFromSqlDataReader(procedureResult);

                            return resultSet;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NpgsqlParameter CreateParameter(string name, object value, NpgsqlDbType type)
        {
            var parameter = new NpgsqlParameter(name, type);
            parameter.Value = value;
            return parameter;

        }
        private IEnumerable<dynamic> ReadFromSqlDataReader(NpgsqlDataReader reader)
        {
            var resultSet = new List<dynamic>();

            if (reader.HasRows)
            {
                var columnsCount = reader.FieldCount;

                while (reader.Read())
                {
                    dynamic entry = new ExpandoObject();

                    for (int column = 0; column < columnsCount; column++)
                    {
                        var columnName = reader.GetName(column);

                        object value = reader.GetValue(column);

                        ((IDictionary<string, object>)entry).Add(columnName, value);
                    }

                    resultSet.Add(entry);
                }
            }

            return resultSet;
        }
        private IEnumerable<TClass> ConvertToTypedSet<TClass>(IEnumerable<dynamic> results) where TClass : new()
        {
            var typedResults = new List<TClass>();

            var properties = typeof(TClass).GetProperties();

            foreach (var result in results)
            {
                var t = new TClass();

                foreach (var property in properties)
                {
                    foreach (var dynamicProperty in ((IDictionary<string, object>)result).Keys)
                    {
                        if (dynamicProperty.ToLower() == property.Name.ToLower())
                        {
                            var value = ((IDictionary<string, object>)result).Single(v => v.Key == dynamicProperty).Value;

                            var typedPropertyResult = GetTypedProperty<TClass>(value, property);

                            property.SetValue(t, typedPropertyResult, null);

                            break;
                        }
                    }
                }

                typedResults.Add(t);
            }

            return typedResults;
        }
        private object GetTypedProperty<TClass>(object value, PropertyInfo property)
        {
            if (value is DBNull)
            {
                if (!IsNullable(property.PropertyType) && property.PropertyType != typeof(string))
                {
                    throw new InvalidOperationException(
                        string.Format("Property '{0}' of type '{1}' should be Nullable.", property.Name,
                                      typeof(TClass).Name));
                }

                return null;
            }

            object typedPropertyResult;

            if (IsNullable(property.PropertyType))
            {
                var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);

                typedPropertyResult = Convert.ChangeType(value, underlyingType);
            }
            else
            {
                typedPropertyResult = Convert.ChangeType(value, property.PropertyType);
            }

            return typedPropertyResult;
        }

     

        private dynamic CoreFunction(Func<string, NpgsqlCommand, dynamic, dynamic> function, Func<string, object[], string> formatQueryFunc, string query, params object[] parameters)
        {
          //  const int maxEchoQueryLength = 200;

            var commandText = formatQueryFunc(query, parameters);

            dynamic result = new ExpandoObject();

         
            try
            {
                using (var sqlConnection = new NpgsqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new NpgsqlCommand(commandText, sqlConnection))
                    {
                        return function(commandText, sqlCommand, result);
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
        private bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private dynamic ReturnScalar(string commandText, NpgsqlCommand sqlCommand, dynamic result)
        {
            var value = sqlCommand.ExecuteScalar();
         
            return value;
        }

        private dynamic ReturnNonQuery(string commandText, NpgsqlCommand sqlCommand, dynamic result)
        {
            var rowsAffected = sqlCommand.ExecuteNonQuery();
           
            return rowsAffected;
        }

        private IEnumerable<dynamic> ReturnMultiple(string commandText, NpgsqlCommand sqlCommand, dynamic result)
        {
            using (NpgsqlDataReader reader = sqlCommand.ExecuteReader())
            {
                var resultSet = ReadFromSqlDataReader(reader);
                 return resultSet;
            }
        }

    

        private dynamic ReturnSingle(string commandText, NpgsqlCommand sqlCommand, dynamic result)
        {
            NpgsqlDataReader reader = sqlCommand.ExecuteReader();

            int rowsCount = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rowsCount++;

                    if (rowsCount > 1)
                    {
                        break;
                    }

                    var columnsCount = reader.FieldCount;

                    for (int column = 0; column < columnsCount; column++)
                    {
                        var columnName = reader.GetName(column);

                        object value = reader.GetValue(column);

                        ((IDictionary<string, object>)result).Add(columnName, value);
                    }
                }
              
            }
            else
            {
               
            }

            return result;
        }
    }
}
