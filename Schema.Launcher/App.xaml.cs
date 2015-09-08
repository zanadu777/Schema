using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Schema.Common;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.Models.IsolatedStorage;
using Schema.Models.SqlServer;
using Schema.ViewModels;
using Schema.Views;

namespace Schema.Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            IGlobalDataModel global = new GlobalDataModel();
            global.LoadConnections();
            foreach (var conn in GlobalData.ConnectionInfos)
            {
                conn.Status = EConnectivityStatus.Unknown;
                conn.IsModified = false;
            }

          
            ShowConnectionManager();
        }



        void OnShowConnectionManagerWindow(object sender, DatabaseConnectionInfoEventArgs e)
        {
            ShowConnectionManager();
        }

        private void ShowConnectionManager()
        {
            Dictionary<string, IConnectivityTester> connector = new Dictionary<string, IConnectivityTester>();
            connector.Add("Sql Server", new ConnectivityTester());
            connector.Add("MySQL", new Models.MySql.ConnectivityTester());
           
            var viewModel = new ConnectionManagerVM(new ConnectionManagerModel(), connector);
            viewModel.OnShowSchemaBrowserWindow += OnShowSchemaBrowserWindow;
            viewModel.OnShowQueryWindow += OnShowQueryWindow;
            var window = new ConnectionManagerWindow(viewModel);
            window.Show();
        }

        void OnShowQueryWindow(object sender, DatabaseConnectionInfoEventArgs e)
        {
            var model = GetQueryModel(e.ConnectionInfo);
            var viewModel = new QueryVM(model);
            viewModel.ConnectionInfo = e.ConnectionInfo;
            var window = new QueryWindow(viewModel);
            window.Show();
        }

        void OnShowSchemaBrowserWindow(object sender, DatabaseConnectionInfoEventArgs e)
        {
            var model =   GetSchemaBrowserModel(e.ConnectionInfo );
            ISchemaBrowserVM viewModel = new SchemaBrowserVM(model,new CodeGeneration.RazorEngine.Generator() , e.ConnectionInfo);
            viewModel.OnShowQueryWindow += OnShowQueryWindow;
            viewModel.OnShowConnectionManagerWindow += OnShowConnectionManagerWindow;
            viewModel.OnShowGenerateTableSqlWindow += ShowGenerateTableSqlWindow;
            viewModel.OnShowCodeGenerationWindow += ShowCodeGenerationWindow;
            var window = new SchemaBrowserWindow(viewModel);
            window.Show();
        }

        private void ShowCodeGenerationWindow(object sender, SchemaObjectEventArgs e)
        {
            var viewmodel = new CodeGenerationVm();
            viewmodel.SchemaObject = e.SchemaObject;
            var window = new CodeGenerationWindow(viewmodel);
            window.Show();
        }

        private void ShowGenerateTableSqlWindow(object sender, DbTableEventArgs e)
        {
            var viewModel = new GenerateTableSqlVm();
            viewModel.Table = e.Table;
            var window = new GenerateTableSqlWindow(viewModel);
            window.Show();
        }

        private ISchemaBrowserModel GetSchemaBrowserModel(DatabaseConnectionInfo connection)
        {
            switch (connection.DatabaseType)
            {
                case "Sql Server":
                    return new SchemaBrowserModel();
                case "MySQL":
                    return new Models.MySql.SchemaBrowserModel();
            }
            return null;
        }

        private IQueryModel GetQueryModel(DatabaseConnectionInfo connection)
        {
            switch (connection.DatabaseType)
            {
                case "Sql Server":
                    return new Models.SqlServer.QueryModel();
                case "MySQL":
                    return new Models.MySql.QueryModel();
            }
            return null;
        }

    }
}
