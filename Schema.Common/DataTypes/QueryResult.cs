using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
