using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.Collections;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Models.SqlServer
{
    public class SchemaBrowserModel : ISchemaBrowserModel
    {
        public void LoadSchemaObjects(ICollection<SchemaObject> objectList, DatabaseConnectionInfo connectionInfo)
        {
            var extractor = new SchemaExtractor();
            objectList.Clear();
            var tables = extractor.GetTables(connectionInfo);
            var columns = extractor.GetTableColumns(connectionInfo);
            AddColumns(tables, columns);
            var primaryKeyColumns = GetPrimaryKeyColumns(connectionInfo);
            AddPrimaryKeys(tables, primaryKeyColumns);
            AddTableCreationSql(tables);
            var views = extractor.GetViews(connectionInfo);
            var viewColumns = extractor.GetViewColumns(connectionInfo);
            AddViewColumns(views, viewColumns);
            var fKeys = GetForeignKeys(connectionInfo);
            AddForeignKeysToTables(tables, fKeys);

            var procedures = extractor.GetStoredProcedures(connectionInfo);
            var parameters = extractor.GetStoredProcedureParameters(connectionInfo);
            AddParametersToStoredProcedures(procedures, parameters);


            foreach (var procedure in procedures.Values)
                objectList.Add(procedure);

            foreach (var view in views.Values)
                objectList.Add(view);

            var sortedTables = from t in tables.Values
                               orderby t.Name
                               select t;
            foreach (var t in sortedTables)
                objectList.Add(t);
        }

        private void AddTableCreationSql(Dictionary<string, DbTable> tables)
        {
            foreach (var table in tables.Values)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Create table {table.Name}(");
                var columns = new RemainderAndLast<DbColumn>(table.Columns);
                foreach (var column in columns.Remainder)
                {
                    var identity = (column.IsIdentity) ? " IDENTITY(1,1) " : " ";
                    var isNull = (column.IsNullable) ? "NULL" : "NOT Null";
                    sb.AppendLine($"{column.Name} {column.DataType}{identity}{isNull},");
                }

                var identity2 = (columns.Last.IsIdentity) ? " IDENTITY(1,1) " : " ";
                var isNull2 = (columns.Last.IsNullable) ? "NULL" : "NOT Null";
                sb.AppendLine($"{columns.Last.Name} {columns.Last.DataType}{identity2}{isNull2}");
                sb.AppendLine(")");
                table.Definition = sb.ToString();
            }
        }

        private void AddParametersToStoredProcedures(Dictionary<string, DbStoredProc> procedures, Dictionary<string, List<DbParameter>> parameters)
        {
            foreach (var key in procedures.Keys)
            {
                var currentProc = procedures[key];
                if (!parameters.ContainsKey(key))
                    continue;

                foreach (var param in parameters[key])
                    currentProc.Parameters.Add(param);
            }
        }

        private void AddForeignKeysToTables(Dictionary<string, DbTable> tables, Dictionary<string, DbForeignKey> fKeys)
        {
            foreach (var foreignKey in fKeys.Values)
            {
                foreach (var tableFullName in tables.Keys)
                {
                    var table = tables[tableFullName];

                    if (foreignKey.ForeignKeyTable == tableFullName)
                    {
                        var keyCols = from col in table.Columns
                                      where foreignKey.ForeignKeyColumns.Contains(col.Name)
                                      select col;

                        foreach (var col in keyCols)
                            col.IsForeignKey = true;
                    }

                    if (foreignKey.PrimaryKeyTable == tableFullName)
                    {
                        var referencedKeyCols = from col in table.Columns
                                      where foreignKey.PrimaryKeyColumns.Contains(col.Name)
                                      select col;

                        foreach (var col in referencedKeyCols)
                            col.IsReferencedPrimaryKey = true;
                    }
                }
            }
        }

        private void AddViewColumns(Dictionary<string, DbView> views, Dictionary<string, List<DbColumn>> viewColumns)
        {
            foreach (var key in viewColumns.Keys)
            {
                var currentView = views[key];
                foreach (var column in viewColumns[key])
                {
                    currentView.Columns.Add(column);
                }
            }
        }

        private void AddPrimaryKeys(Dictionary<string, DbTable> tables, Dictionary<string, List<string>> primaryKeyColumns)
        {
            foreach (var key in primaryKeyColumns.Keys)
            {
                var currentTable = tables[key];
                foreach (var pk in primaryKeyColumns[key])
                {
                    var col = (from DbColumn c in currentTable.Columns
                               where c.Name == pk
                               select c).FirstOrDefault();

                    if (col != null)
                        col.IsInPrimaryKey = true;
                }
            }
        }

        private void AddColumns(Dictionary<string, DbTable> tables, Dictionary<string, List<DbColumn>> columns)
        {
            foreach (var key in columns.Keys)
            {
                var currentTable = tables[key];
                foreach (var column in columns[key])
                {
                    currentTable.Columns.Add(column);
                }
            }
        }


        private Dictionary<string, List<string>> GetPrimaryKeyColumns(DatabaseConnectionInfo connectionInfo)
        {
            var sql = @"SELECT
SCHEMA_NAME(t.schema_id)  + '.' +    t.name     as tableFullName,
	col_name(c.object_id, c.column_id)	AS columnName,
	c.key_ordinal				AS ORDINAL_POSITION
FROM
	sys.key_constraints k JOIN sys.index_columns c
		ON c.object_id = k.parent_object_id
		AND c.index_id = k.unique_index_id
	JOIN sys.tables t ON t.object_id = k.parent_object_id";
            var primaryKeys = new Dictionary<string, List<string>>();

            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var tableFullNamePos = reader.GetOrdinal("tableFullName");
                    var columnNamePos = reader.GetOrdinal("columnName");
                    while (reader.Read())
                    {
                        var fullTableName = reader.GetString(tableFullNamePos);

                        if (!primaryKeys.ContainsKey(fullTableName))
                            primaryKeys.Add(fullTableName, new List<string>());

                        primaryKeys[fullTableName].Add(reader.GetString(columnNamePos));

                    }
                }
            }
            return primaryKeys;
        }

        private Dictionary<string, DbForeignKey> GetForeignKeys(DatabaseConnectionInfo connectionInfo)
        {
            var fKeys = new Dictionary<string, DbForeignKey>();

            var sql = @"select	SCHEMA_NAME(sof.schema_id) + '.' + sof.name as ConstraintName ,
		SCHEMA_NAME(sop.schema_id) + '.' + sop.name as ForeignKeyTable, 
		SCHEMA_NAME(sor.schema_id) + '.' +sor.name  as PrimaryKeyTable, 
		scp.name as ForeignKeyColumn, 
		scf.name as PrimaryKeyColumn
from sys.foreign_key_columns  fk

inner join Sys.objects sop on fk.parent_object_id = sop.object_id
inner join Sys.objects sof on fk.constraint_object_id = sof.object_id
inner join Sys.objects sor on fk.referenced_object_id = sor.object_id
inner join Sys.Columns scp on fk.parent_column_id = scp.column_id and fk.parent_object_id = scp.object_id
inner join Sys.Columns scf on fk.referenced_column_id = scf.column_id and fk.referenced_object_id = scf.object_id";
            var primaryKeys = new Dictionary<string, List<string>>();

            using (var conn = new SqlConnection(connectionInfo.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();
                    var constraintNamePos = reader.GetOrdinal("ConstraintName");
                    var foreignKeyTablePos = reader.GetOrdinal("ForeignKeyTable");
                    var primaryKeyTablePos = reader.GetOrdinal("PrimaryKeyTable");
                    var foreignKeyColumnPos = reader.GetOrdinal("ForeignKeyColumn");
                    var primaryKeyColumnPos = reader.GetOrdinal("PrimaryKeyColumn");
                    while (reader.Read())
                    {
                        var constraintName = reader.GetString(constraintNamePos);
                        DbForeignKey fKey;
                        if (!fKeys.ContainsKey(constraintName))
                        {
                            fKey = new DbForeignKey
                            {
                                ConstraintFullName = constraintName,
                                ForeignKeyTable = reader.GetString(foreignKeyTablePos),
                                PrimaryKeyTable = reader.GetString(primaryKeyTablePos)
                            };
                            fKeys.Add(constraintName, fKey);
                        }
                        fKeys[constraintName].Columns.Add(new DbForeignKeyColumn
                        {
                            PrimaryKeyColumn = reader.GetString(primaryKeyColumnPos),
                            ForeignKeyColumn = reader.GetString(foreignKeyColumnPos),
                        });
                    }
                }
            }
            return fKeys;
        }
    }
}
