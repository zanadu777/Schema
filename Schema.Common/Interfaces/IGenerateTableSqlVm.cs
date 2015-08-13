using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IGenerateTableSqlVm
    {
        DbTable Table { get; set; }

        DataTable Columns { get; set; }
    }
}
