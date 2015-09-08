using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Models.MySql
{
    class SchemaExtractor : ISchemaExtractor
    {
        public Dictionary<string, DbTable> GetTables(DatabaseConnectionInfo connectionInfo)
        {
            var tables = new Dictionary<string, DbTable>();

            var sql = @"SELECT table_name as name FROM information_schema.tables WHERE table_type = 'base table'";
            using (var conn = new MySqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                 
                    var namePos = reader.GetOrdinal("name");
                    
                    while (reader.Read())
                    {

                        tables.Add(reader.GetString(namePos), new DbTable
                                                                  {
                                                                      Name = reader.GetString(namePos),
                                                                      
                                                                  });

                    }
                }
            }
            return tables;
        }

        public Dictionary<string, List<DbColumn>> GetTableColumns(DatabaseConnectionInfo connectionInfo)
        {
            var columns = new Dictionary<string, List<DbColumn>>();
            var sql = @"SELECT TABLE_NAME as fullTableName , c.COLUMN_NAME as columnName, c.ORDINAL_POSITION as ordinal , c.IS_NULLABLE as is_nullable,
DATA_TYPE  as datatype,c.CHARACTER_MAXIMUM_LENGTH as  max_length
, c.NUMERIC_PRECISION  
FROM INFORMATION_SCHEMA.COLUMNS c
where TABLE_SCHEMA != 'information_schema'
order by TABLE_NAME , ORDINAL_POSITION 
";

            using (var conn = new MySqlConnection(connectionInfo.ConnectionString))
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
                    //var is_identityPos = reader.GetOrdinal("is_identity");
                    var datatypePos = reader.GetOrdinal("datatype");
                    var max_lengthPos = reader.GetOrdinal("max_length");
                    while (reader.Read())
                    {
                        var fullTableName = reader.GetString(fullTableNamePos);

                        if (!columns.ContainsKey(fullTableName))
                            columns.Add(fullTableName, new List<DbColumn>());

                        var col = new DbColumn
                                  {
                                      Name = reader.GetString(columnNamePos),
                                      IsNullable = (reader.GetString(is_nullablePos) == "YES"),
                                      //IsIdentity = reader.GetBoolean(is_identityPos),
                                      DataType = reader.GetString(datatypePos),
                                      MaxLength = GetMaxLength(reader.GetValue(max_lengthPos))
                                  };
                        col.DisplayDataType = DisplayType(col);
                        columns[fullTableName].Add(col);
                    }
                }
            }
            return columns;
        }

        private int GetMaxLength(object obj)
        {
            var dbnull = obj as DBNull;
            if (dbnull != null)
                return 0;

            return (int)(ulong)obj;
        }

        public Dictionary<string, DbView> GetViews(DatabaseConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<DbColumn>> GetViewColumns(DatabaseConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, DbStoredProc> GetStoredProcedures(DatabaseConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<DbParameter>> GetStoredProcedureParameters(DatabaseConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
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
