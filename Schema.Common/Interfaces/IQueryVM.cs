using System;
using System.Windows.Input;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IQueryVM
    {
        string Query { get; set; }
        DatabaseConnectionInfo ConnectionInfo { get; set; }

        ICommand ExecuteQueryCommand { get; set; }

        QueryResult QueryResult { get; set; }

        TimeSpan QueryTime { get; set; }

        ICommand ExportToExcelCommand { get; set; }

        ICommand GenerateSnippetCommand { get; set; }

        string GeneratedSnippet { get; set; }
    }
}
