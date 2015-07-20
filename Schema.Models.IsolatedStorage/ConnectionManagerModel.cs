using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.Models.IsolatedStorage.ExtensionMethods;

namespace Schema.Models.IsolatedStorage
{
    public class ConnectionManagerModel : IConnectionManagerModel
    {
        private const string ConnectionsPath = "connections.json";

        public void LoadConnections(ObservableCollection<DatabaseConnectionInfo> connectionInfos)
        {
            string json;
            using (var isoStore = RootStore())
                json = isoStore.ReadAllText(ConnectionsPath);

            if (string.IsNullOrWhiteSpace(json)) return;
            try
            {
                var storedConnections = JsonConvert.DeserializeObject<ObservableCollection<DatabaseConnectionInfo>>(json);

                foreach (var con in storedConnections)
                    connectionInfos.Add(con);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(json);
            }
        }

        public void SaveConnections(
             ObservableCollection<DatabaseConnectionInfo> connectionInfos)
        {
            if (connectionInfos.Count == 0)
            {
                using (var isoStore = RootStore())
                {
                    isoStore.DeleteFile(ConnectionsPath);
                }
                return;
            }

            var json = JsonConvert.SerializeObject(connectionInfos);

            using (var isoStore = RootStore())
            {
                isoStore.WriteAllText(ConnectionsPath, json);
            }
        }

        private static IsolatedStorageFile RootStore()
        {
            return
                IsolatedStorageFile.GetStore(
                    IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
        }
    }
}
