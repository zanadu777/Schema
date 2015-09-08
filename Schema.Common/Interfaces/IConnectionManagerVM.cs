using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IConnectionManagerVM
    {
        ICommand ConnectionSelectedCommand { get; set; }
        ICommand ConnectionsSelectedCommand { get; set; }

        event EventHandler<DatabaseConnectionInfoEventArgs> OnConnectionSelected;


        ICommand DuplicateConnectionsCommand { get; set; }
        ICommand LoadConnectionsCommand { get; }

        ICommand SaveConnectionsCommand { get; }

        ICommand CreateConnectionCommand { get; set; }

        ICommand CopyConnectionCommand { get; set; }

        ICommand RemoveConnectionCommand { get; set; }

        ICommand RemoveConnectionsCommand { get; set; }

        ICommand RemoveAllConnectionsCommand { get; set; }

        ICommand ImportConnectionsCommand { get; set; }

        ICommand ExportConnectionsCommand { get; set; }
        ICommand TestConnectionCommand { get; }
        ICommand TestSelectedConnectionsCommand { get; }

        ICommand TestAllConnectionsCommand { get; }

        ObservableCollection<DatabaseConnectionInfo> Connections { get; set; }

        ObservableCollection<DatabaseConnectionInfo> SelectedConnections { get; set; }

        DatabaseConnectionInfo SelectedConnection { get; set; }
        ICommand ShowQueryWindowCommand { get; }

        event EventHandler<DatabaseConnectionInfoEventArgs> OnShowQueryWindow;

        ICommand ShowSchemaBrowserWindowCommand { get; }

        event EventHandler<DatabaseConnectionInfoEventArgs> OnShowSchemaBrowserWindow;

    }
}
