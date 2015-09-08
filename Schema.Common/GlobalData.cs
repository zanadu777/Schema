using System.Collections.ObjectModel;
using Schema.Common.DataTypes;

namespace Schema.Common
{
    public static class GlobalData
    {
        static GlobalData()
        {
            ConnectionInfos = new ObservableCollection<DatabaseConnectionInfo>();
        }
        public static ObservableCollection<DatabaseConnectionInfo> ConnectionInfos { get; set; }
    }
}
