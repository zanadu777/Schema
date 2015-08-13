using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.ViewModels
{
    public class GenerateTableSqlVm:IGenerateTableSqlVm 
    {
        private DbTable table;

        public DbTable Table
        {
            get { return table; }
            set
            {
                table = value;
                Columns = table.ColumnDataTable();
            }
        }

        public DataTable Columns { get; set; }
    }
}
