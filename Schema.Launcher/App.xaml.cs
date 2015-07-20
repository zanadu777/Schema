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

            //var model = new SchemaBrowserModel();
            //ISchemaBrowserVM viewModel = new SchemaBrowserVM(model, GlobalData.ConnectionInfos[1]);
            //viewModel.OnShowQueryWindow += OnShowQueryWindow;
            //viewModel.OnShowConnectionManagerWindow += OnShowConnectionManagerWindow;
            //MainWindow = new SchemaBrowserWindow(viewModel);
            //MainWindow.Show();
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

            var viewModel = new ConnectionManagerVM(new ConnectionManagerModel(), connector);
            viewModel.OnShowSchemaBrowserWindow += OnShowSchemaBrowserWindow;
            viewModel.OnShowQueryWindow += OnShowQueryWindow;
            var window = new ConnectionManagerWindow(viewModel);
            window.Show();
        }

        void OnShowQueryWindow(object sender, DatabaseConnectionInfoEventArgs e)
        {
            var model = new QueryModel();
            var viewModel = new QueryVM(model);
            viewModel.ConnectionInfo = e.ConnectionInfo;
            var window = new QueryWindow(viewModel);
            window.Show();
        }

        void OnShowSchemaBrowserWindow(object sender, DatabaseConnectionInfoEventArgs e)
        {
            var model = new SchemaBrowserModel();
            ISchemaBrowserVM viewModel = new SchemaBrowserVM(model, e.ConnectionInfo);
            viewModel.OnShowQueryWindow += OnShowQueryWindow;
            viewModel.OnShowConnectionManagerWindow += OnShowConnectionManagerWindow;
            var window = new SchemaBrowserWindow(viewModel);
            window.Show();
        }

    }
}
