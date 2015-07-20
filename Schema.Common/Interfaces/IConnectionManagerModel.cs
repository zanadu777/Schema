using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface IConnectionManagerModel
    {
        void LoadConnections(ObservableCollection<DatabaseConnectionInfo> connectionInfos);
        void SaveConnections(ObservableCollection<DatabaseConnectionInfo> connectionInfos);
    }
}
