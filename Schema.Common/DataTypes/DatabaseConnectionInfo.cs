using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
    public class DatabaseConnectionInfo : INotifyPropertyChanged
    {
        private EConnectivityStatus status;
        private bool isModified;
        private string databaseType;
        private string friendlyName;
        private string connectionString;
        private Guid objectKey;

        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                SetProperty(ref connectionString, value);
                IsModified = true;
            }
        }

        public string FriendlyName
        {
            get { return friendlyName; }
            set
            {
                SetProperty(ref friendlyName, value);
                IsModified = true;
            }
        }

        public String DatabaseType
        {
            get { return databaseType; }
            set
            {
                SetProperty(ref databaseType, value);
                IsModified = true;
            }
        }

        public bool IsModified
        {
            get { return isModified; }
            set
            {
                SetProperty(ref isModified, value);
            }
        }

        public EConnectivityStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        public Guid ObjectKey
        {
            get { return objectKey; }
            set { SetProperty(ref objectKey, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }

    }
}
