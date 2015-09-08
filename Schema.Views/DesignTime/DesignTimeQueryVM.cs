using System.Windows.Input;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Views.DesignTime
{
    class DesignTimeQueryVM : IQueryVM
    {

        public DesignTimeQueryVM()
        {
            ConnectionInfo = new DatabaseConnectionInfo();
            QueryResult = new QueryResult();

        }
        public string Query { get; set; }

        public DatabaseConnectionInfo ConnectionInfo { get; set; }



        public ICommand ExecuteQueryCommand { get; set; }

        public QueryResult QueryResult { get; set; }


        public System.TimeSpan QueryTime { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand GenerateSnippetCommand { get; set; }
        public string GeneratedSnippet { get; set; }
    }
}
