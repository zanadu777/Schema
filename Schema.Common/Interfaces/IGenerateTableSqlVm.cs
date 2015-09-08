using System.Data;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IGenerateTableSqlVm
    {
        DbTable Table { get; set; }

        DataTable Columns { get; set; }
    }
}
