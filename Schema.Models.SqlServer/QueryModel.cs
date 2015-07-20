using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Models.SqlServer
{
    public class QueryModel : IQueryModel
    {
        public QueryResult Execute(string query, DatabaseConnectionInfo connectionInfo)
        {
            var result = new QueryResult();
            Stopwatch swatch = new Stopwatch();
            swatch.Start();
            try
            {
                using (var conn = new SqlConnection(connectionInfo.ConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        var reader = cmd.ExecuteReader();
                        var fieldCount = reader.FieldCount;
                        DataTable dt = new DataTable();
                        for (int i = 0; i < fieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            var ft = reader.GetFieldType(i);
                            dt.Columns.Add(new DataColumn(name, ft));

                        }
                        while (reader.Read())
                        {
                            var row = dt.NewRow();
                            for (int i = 0; i < fieldCount; i++)
                            {
                                row[i] = reader.GetValue(i);
                            }

                            dt.Rows.Add(row);
                        }
                        result.DataTable = dt;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            result.QueryTimeSpan = swatch.Elapsed;
            swatch.Stop();
            return result;
        }
    }
}
