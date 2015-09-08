using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Models.SqlServer
{
    [Serializable]
    public class SchemaExtractor : ISchemaExtractor
    {
        public Dictionary<string, DbTable> GetTables(DatabaseConnectionInfo connectionInfo)
        {
            var tables = new Dictionary<string, DbTable>();

            var sql = @"select sc.Name + '.'+ so.name as fullName, so.name, sc.name as schemaName from sys.objects so
 inner join sys.schemas  sc on so.schema_id = sc.schema_id
where type in ('U')";
            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var fullNamePos = reader.GetOrdinal("fullName");
                    var namePos = reader.GetOrdinal("name");
                    var schemaNamePos = reader.GetOrdinal("schemaName");
                    while (reader.Read())
                    {

                        tables.Add(reader.GetString(fullNamePos), new DbTable
                        {
                            Name = reader.GetString(namePos),
                            SchemaName = reader.GetString(schemaNamePos)
                        });

                    }
                }
            }
            return tables;
        }

        public Dictionary<string, List<DbColumn>> GetTableColumns(DatabaseConnectionInfo connectionInfo)
        {
            var columns = new Dictionary<string, List<DbColumn>>();
            var sql = @"select SCHEMA_NAME(so.schema_id) + '.'+ so.name as fullTableName ,c.name as columnName,c.column_id as ordinal, c.is_nullable , c.is_identity, c.is_computed  ,TYPE_NAME(c.system_type_id) as datatype,c.max_length, c.precision
  from sys.columns c 
 inner join sys.objects so on so.object_id = c.object_id
 where so.type in ('U')
 order by so.object_id, c.column_id";

            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var fullTableNamePos = reader.GetOrdinal("fullTableName");
                    var columnNamePos = reader.GetOrdinal("columnName");
                    var is_nullablePos = reader.GetOrdinal("is_nullable");
                    var is_identityPos = reader.GetOrdinal("is_identity");
                    var datatypePos = reader.GetOrdinal("datatype");
                    var max_lengthPos = reader.GetOrdinal("max_length");
                    var ordinalPos = reader.GetOrdinal("ordinal");
                    while (reader.Read())
                    {
                        var fullTableName = reader.GetString(fullTableNamePos);

                        if (!columns.ContainsKey(fullTableName))
                            columns.Add(fullTableName, new List<DbColumn>());

                        var col = new DbColumn
                                  {
                                      Name = reader.GetString(columnNamePos),
                                      IsNullable = reader.GetBoolean(is_nullablePos),
                                      IsIdentity = reader.GetBoolean(is_identityPos),
                                      DataType = reader.GetString(datatypePos),
                                      MaxLength = (Int16) reader.GetValue(max_lengthPos),
                                      Ordinal = reader.GetInt32(ordinalPos)
                                  };
                        col.DisplayDataType = DisplayType(col);
                        columns[fullTableName].Add(col);
                    }
                }
            }
            return columns;
        }

        public Dictionary<string, DbView> GetViews(DatabaseConnectionInfo connectionInfo)
        {
            var views = new Dictionary<string, DbView>();
            string sql = @"select SCHEMA_NAME(o.schema_id) + '.' +  o.name as fullname, 
SCHEMA_NAME(o.schema_id) as schemaName, o.name,  m.definition
from sys.objects     o
join sys.sql_modules m on m.object_id = o.object_id
and o.type= 'V'";

            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var fullNamePos = reader.GetOrdinal("fullName");
                    var namePos = reader.GetOrdinal("name");
                    var schemaNamePos = reader.GetOrdinal("schemaName");
                    var definitionPos = reader.GetOrdinal("definition");
                    while (reader.Read())
                    {

                        views.Add(reader.GetString(fullNamePos), new DbView
                        {
                            Name = reader.GetString(namePos),
                            SchemaName = reader.GetString(schemaNamePos),
                            Definition = reader.GetString(definitionPos).Trim()
                        });

                    }
                }
            }
            return views;
        }

        public Dictionary<string, List<DbColumn>> GetViewColumns(DatabaseConnectionInfo connectionInfo)
        {
            var columns = new Dictionary<string, List<DbColumn>>();
            var sql = @"select SCHEMA_NAME(so.schema_id) + '.'+ so.name as fullTableName ,c.name as columnName,c.column_id as ordinal, c.is_nullable    ,TYPE_NAME(c.system_type_id) as datatype,c.max_length, c.precision
  from sys.columns c 
 inner join sys.objects so on so.object_id = c.object_id
 where so.type in ('V')
 order by so.object_id, c.column_id";

            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var fullTableNamePos = reader.GetOrdinal("fullTableName");
                    var columnNamePos = reader.GetOrdinal("columnName");
                    var is_nullablePos = reader.GetOrdinal("is_nullable");
                    var datatypePos = reader.GetOrdinal("datatype");
                    var max_lengthPos = reader.GetOrdinal("max_length");
                    var ordinalPos = reader.GetOrdinal("ordinal");
                    while (reader.Read())
                    {
                        var fullTableName = reader.GetString(fullTableNamePos);

                        if (!columns.ContainsKey(fullTableName))
                            columns.Add(fullTableName, new List<DbColumn>());
                        var col = new DbColumn
                                  {
                                      Name = reader.GetString(columnNamePos),
                                      IsNullable = reader.GetBoolean(is_nullablePos),
                                      DataType = reader.GetString(datatypePos),
                                      MaxLength = (Int16) reader.GetValue(max_lengthPos),
                                      Ordinal = reader.GetInt32(ordinalPos)
                                  };
                        col.DisplayDataType = DisplayType(col);
                        columns[fullTableName].Add(col);
                    }
                }
            }
            return columns;
        }

        public Dictionary<string, DbStoredProc> GetStoredProcedures(DatabaseConnectionInfo connectionInfo)
        {
            var sql = @" select SCHEMA_NAME(p.schema_id) + '.' +  p.name as fullname,p.name ,SCHEMA_NAME(p.schema_id) as schemaName, m.definition from sys.procedures p inner join
 sys.sql_modules m on p.object_id = m.object_id";

            var procedures = new Dictionary<string, DbStoredProc>();
            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var fullNamePos = reader.GetOrdinal("fullName");
                    var namePos = reader.GetOrdinal("name");
                    var schemaNamePos = reader.GetOrdinal("schemaName");
                    var definitionPos = reader.GetOrdinal("definition");

                    while (reader.Read())
                    {

                        procedures.Add(reader.GetString(fullNamePos), new DbStoredProc()
                        {
                            Name = reader.GetString(namePos),
                            SchemaName = reader.GetString(schemaNamePos),
                            Definition = reader.GetString(definitionPos).Trim()
                        });
                    }
                }
            }
            return procedures;
        }

        public Dictionary<string, List<DbParameter>> GetStoredProcedureParameters(DatabaseConnectionInfo connectionInfo)
        {
            var parameters = new Dictionary<string, List<DbParameter>>();
            var sql = @"select Specific_Schema + '.' +  Specific_Name as Full_Procedure_Name,
PARAMETER_NAME, ORDINAL_POSITION,   DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, PARAMETER_MODE
 from information_schema.parameters";

            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var fullProcedureNamePos = reader.GetOrdinal("Full_Procedure_Name");
                    var ordinalPos = reader.GetOrdinal("ORDINAL_POSITION");
                    var parameterName  = reader.GetOrdinal("PARAMETER_NAME");
                    var datatypePos = reader.GetOrdinal("DATA_TYPE");
                    var max_lengthPos = reader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
                    while (reader.Read())
                    {
                        var fullName = reader.GetString(fullProcedureNamePos);

                        if (!parameters.ContainsKey(fullName))
                            parameters.Add(fullName, new List<DbParameter>());

                        parameters[fullName].Add(new DbParameter
                        {
                            Name = reader.GetString(parameterName),
                            Ordinal = reader.GetInt32( ordinalPos),
                            DataType = reader.GetString(datatypePos),
                            MaxLength = GetMaxLength(reader.GetValue(max_lengthPos))
                        });
                    }
                }
            }
            return parameters;
        }


        private int GetMaxLength(object obj)
        {
            if (obj is DBNull)
            return 0;

            if (obj is int)
                return (int) obj;
            else
            {
                return -2;
            }
        }

        public string DisplayType(DbColumn column)
        {
            var dbDataType = column.DataType;
            switch (dbDataType)
            {
                case "nvarchar":
                case "nchar":
                    if (column.MaxLength == -1)
                        dbDataType += "(MAX)";
                    else
                        dbDataType += "(" + column.MaxLength / 2 + ")";
                    break;
                case "varchar":
                case "char":
                case "varbinary":
                    if (column.MaxLength == -1)
                        dbDataType += "(MAX)";
                    else
                        dbDataType += "(" + column.MaxLength + ")";
                    break;
            }

            if (column.IsIdentity) dbDataType += "(identity)";
            return dbDataType;
        }
    }
}
