using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            var views = extractor.GetViews(connectionInfo);
            var viewColumns = extractor.GetViewColumns(connectionInfo);
            AddViewColumns(views, viewColumns);


            var procedures = extractor.GetStoredProcedures(connectionInfo);

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
                               select c).First();
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


    }

}
