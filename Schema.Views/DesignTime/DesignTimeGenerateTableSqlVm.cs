using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Views.DesignTime
{
    class DesignTimeGenerateTableSqlVm:IGenerateTableSqlVm
    {
        public DbTable Table { get; set; }
        public DataTable Columns { get; set; }
    }
}
