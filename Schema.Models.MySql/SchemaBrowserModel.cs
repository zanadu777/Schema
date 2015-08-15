using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Models.MySql
{
    public class SchemaBrowserModel:ISchemaBrowserModel
    {
        public void LoadSchemaObjects(ICollection<SchemaObject> objectList, DatabaseConnectionInfo connectionInfo)
        {
            var extractor = new SchemaExtractor();
            objectList.Clear();
            var tables = extractor.GetTables(connectionInfo);
            var columns = extractor.GetTableColumns(connectionInfo);
            AddColumns(tables, columns);

            var sortedTables = from t in tables.Values
                               orderby t.Name
                               select t;
            foreach (var t in sortedTables)
                objectList.Add(t);
        }


        private void AddColumns(Dictionary<string, DbTable> tables, Dictionary<string, List<DbColumn>> columns)
        {
            foreach (var key in columns.Keys)
            {
                if (!tables.ContainsKey(key))
                    Debug.Write(key);
                var currentTable = tables[key];
                foreach (var column in columns[key])
                {
                    currentTable.Columns.Add(column);
                }
            }
        }
    }
}
