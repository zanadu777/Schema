using System.Collections.ObjectModel;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IConnectionManagerModel
    {
        void LoadConnections(ObservableCollection<DatabaseConnectionInfo> connectionInfos);
        void SaveConnections(ObservableCollection<DatabaseConnectionInfo> connectionInfos);
    }
}
