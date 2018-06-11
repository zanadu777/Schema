using System.Data;
using System.Windows.Input;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IGenerateTableSqlVm
    {
        DbTable Table { get; set; }

        DataTable Columns { get; set; }

        ICommand GenerateSqlCommand { get; set; }


        string GenratedSql { get; set; }
    }
}
