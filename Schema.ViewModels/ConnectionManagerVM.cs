using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Schema.Common;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.ViewModels.ExtensionMethods;

namespace Schema.ViewModels
{
    public class ConnectionManagerVM : BindableBase, IConnectionManagerVM
    {
        private IConnectionManagerModel model;
        private Dictionary<string, IConnectivityTester> testerDict;
        private DatabaseConnectionInfo selectedConnection;

        public ConnectionManagerVM(IConnectionManagerModel iConnectionManagerModel, Dictionary<string, IConnectivityTester> testers)
        {
            model = iConnectionManagerModel;
            testerDict = testers;
            LoadConnectionsCommand = new DelegateCommand(LoadConnections);
            CreateConnectionCommand = new DelegateCommand(CreateConnection);
            SaveConnectionsCommand = new DelegateCommand(SaveConnections);
            TestConnectionCommand = new DelegateCommand<DatabaseConnectionInfo>(TestConnection);
            TestAllConnectionsCommand = new DelegateCommand(TestAllConnections);
            RemoveConnectionsCommand = new DelegateCommand<IList>(RemoveConnections);
            RemoveAllConnectionsCommand = new DelegateCommand(RemoveAllConnections);
            DuplicateConnectionsCommand = new DelegateCommand(DuplicateConnection);

            //Selection Commands
            ConnectionSelectedCommand = new DelegateCommand<DatabaseConnectionInfo>(x => SelectedConnection = x);
            // ConnectionsSelectedCommand = new DelegateCommand<IList>(x => SelectedConnections = x.ToObservableCollection<DatabaseConnectionInfo>());
            ConnectionsSelectedCommand = new DelegateCommand<IList>(ConnectionsSelected);

            //new windows and Dialogs
            ShowQueryWindowCommand = new DelegateCommand<DatabaseConnectionInfo>(x => OnShowQueryWindow.Raise(this, new DatabaseConnectionInfoEventArgs(x)));
            ShowSchemaBrowserWindowCommand = new DelegateCommand<DatabaseConnectionInfo>(x => OnShowSchemaBrowserWindow.Raise(this, new DatabaseConnectionInfoEventArgs(x)));

            //initialize collection
            SelectedConnections = new ObservableCollection<DatabaseConnectionInfo>();

        }

        private void ConnectionsSelected(IList obj)
        {
            SelectedConnections = obj.ToObservableCollection<DatabaseConnectionInfo>();
        }

        private void DuplicateConnection()
        {
            if (SelectedConnections.Contains(SelectedConnection) == false && SelectedConnection != null)
                SelectedConnections.Add(SelectedConnection);

            foreach (var origConn in SelectedConnections  )
            {
                var newconn = new DatabaseConnectionInfo
                              {
                                  ObjectKey = new Guid(),
                                  FriendlyName = origConn.FriendlyName + " copy",
                                  DatabaseType = origConn.DatabaseType,
                                  ConnectionString = origConn.ConnectionString,
                                  Status = origConn.Status
                              };
                Connections.Add(newconn);
            }
        }

        private void RemoveAllConnections()
        {
            Connections.Clear();
            SaveConnections();
        }

        private void RemoveConnections(IList connections)
        {
            var conArray = (from DatabaseConnectionInfo c in connections
                            select c).ToList();
            foreach (var conn in conArray)
                Connections.Remove(conn);

            SaveConnections();
        }


        private async void TestAllConnections()
        {
            foreach (var conn in Connections)
            {
                conn.Status = EConnectivityStatus.Testing;
            }
            foreach (var conn in Connections)
            {
                var result = await testerDict[conn.DatabaseType].TestConnectivityAsync(conn.ConnectionString);
                conn.Status = (result) ? EConnectivityStatus.Connected : EConnectivityStatus.Disconnected;
            }
        }

        private async void TestConnection(DatabaseConnectionInfo conn)
        {
            conn.Status = EConnectivityStatus.Testing;
            var result = await testerDict[conn.DatabaseType].TestConnectivityAsync(conn.ConnectionString);
            conn.Status = (result) ? EConnectivityStatus.Connected : EConnectivityStatus.Disconnected;
        }

        private void SaveConnections()
        {
            foreach (var conn in Connections)
            {
                conn.IsModified = false;
            }
            model.SaveConnections(Connections);
        }

        private void CreateConnection()
        {
            var conn = new DatabaseConnectionInfo();
            conn.FriendlyName = "New Connection String";
            conn.ConnectionString = "Connection string";
            conn.Status = EConnectivityStatus.Unknown;
            conn.ObjectKey = new Guid();
            Connections.Insert(0, conn);
        }

        private void LoadConnections()
        {
            var key = SelectedConnection.ObjectKey;
            Connections.Clear();
            model.LoadConnections(Connections);
            foreach (var connection in Connections)
            {
                connection.Status = EConnectivityStatus.Unknown;
                connection.IsModified = false;
                if (connection.ObjectKey == key)
                {
                    SelectedConnection = connection;
                }
            }
            OnConnectionSelected.Raise(this, new DatabaseConnectionInfoEventArgs(SelectedConnection));

        }

        public ICommand LoadConnectionsCommand { get; set; }



        public ICommand TestConnectionCommand { get; set; }

        public ICommand TestSelectedConnectionsCommand { get; set; }

        public ICommand TestAllConnectionsCommand { get; set; }


        public ObservableCollection<DatabaseConnectionInfo> Connections
        {
            get { return GlobalData.ConnectionInfos; }
            set { GlobalData.ConnectionInfos = value; }
        }



        public ICommand SaveConnectionsCommand { get; set; }

        public ICommand RemoveAllConnectionsCommand { get; set; }

        public ICommand ImportConnectionsCommand { get; set; }


        public ICommand ExportConnectionsCommand { get; set; }


        public ICommand CreateConnectionCommand { get; set; }

        public ICommand CopyConnectionCommand { get; set; }

        public ICommand RemoveConnectionCommand { get; set; }


        public ICommand ShowQueryWindowCommand { get; set; }

        public event EventHandler<DatabaseConnectionInfoEventArgs> OnShowQueryWindow;

        public ICommand ShowSchemaBrowserWindowCommand { get; set; }

        public event EventHandler<DatabaseConnectionInfoEventArgs> OnShowSchemaBrowserWindow;


        public ICommand RemoveConnectionsCommand { get; set; }

        public ICommand ConnectionSelectedCommand { get; set; }

        public ICommand ConnectionsSelectedCommand { get; set; }

        public ObservableCollection<DatabaseConnectionInfo> SelectedConnections { get; set; }

        public DatabaseConnectionInfo SelectedConnection
        {
            get { return selectedConnection; }
            set
            {
                SetProperty(ref this.selectedConnection, value);
                this.OnPropertyChanged(() => this.SelectedConnection);
            }
        }


        public event EventHandler<DatabaseConnectionInfoEventArgs> OnConnectionSelected;


        public ICommand DuplicateConnectionsCommand { get; set; }
    }
}
