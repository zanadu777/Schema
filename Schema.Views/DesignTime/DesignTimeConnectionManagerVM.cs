using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Views.DesignTime
{
    class DesignTimeConnectionManagerVM : IConnectionManagerVM
    {
        public DesignTimeConnectionManagerVM()
        {
            Connections = new ObservableCollection<DatabaseConnectionInfo>();
            SelectedConnections = new ObservableCollection<DatabaseConnectionInfo>();
        }
        public ICommand LoadConnectionsCommand { get; set; }


        public ICommand TestConnectionCommand { get; set; }

        public ICommand TestSelectedConnectionsCommand { get; set; }

        public ICommand TestAllConnectionsCommand { get; set; }


        public ObservableCollection<DatabaseConnectionInfo> Connections { get; set; }


        public ICommand SaveConnectionsCommand { get; set; }

        public ICommand RemoveAllConnectionsCommand { get; set; }

        public ICommand ImportConnectionsCommand { get; set; }


        public ICommand ExportConnectionsCommand { get; set; }


        public ICommand CreateConnectionCommand { get; set; }

        public ICommand CopyConnectionCommand { get; set; }

        public ICommand RemoveConnectionCommand { get; set; }


        public ICommand ShowQueryWindowCommand { get; set; }

        public event EventHandler<Common.CustomEventArgs.DatabaseConnectionInfoEventArgs> OnShowQueryWindow;

        public ICommand ShowSchemaBrowserWindowCommand { get; set; }

        public event EventHandler<Common.CustomEventArgs.DatabaseConnectionInfoEventArgs> OnShowSchemaBrowserWindow;


        public ICommand RemoveConnectionsCommand { get; set; }

        public ICommand ConnectionSelectedCommand { get; set; }


        public ICommand ConnectionsSelectedCommand { get; set; }


        public ObservableCollection<DatabaseConnectionInfo> SelectedConnections { get; set; }


        public DatabaseConnectionInfo SelectedConnection{ get; set; }




        public event EventHandler<DatabaseConnectionInfoEventArgs> OnConnectionSelected;


        public ICommand DuplicateConnectionsCommand { get; set; }
    }
}
