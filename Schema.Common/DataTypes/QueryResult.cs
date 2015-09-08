using System;
using System.Data;

namespace Schema.Common.DataTypes
{
    public class QueryResult
    {
        public QueryResult()
        {
            DataTable = new DataTable();
        }
        public DataTable DataTable { get; set; }

        public TimeSpan QueryTimeSpan { get; set; }
    }
}
