using System;
using Schema.Common.DataTypes;

namespace Schema.Common.CustomEventArgs
{
    public class DatabaseConnectionInfoEventArgs : EventArgs
    {

        public DatabaseConnectionInfo ConnectionInfo { get; private set; }
      
        public DatabaseConnectionInfoEventArgs(DatabaseConnectionInfo databaseConnectionInfo)
        {
            ConnectionInfo = databaseConnectionInfo;
        }
    }
}
