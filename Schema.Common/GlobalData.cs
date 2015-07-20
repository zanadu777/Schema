using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
