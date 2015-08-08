using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Schema.Common;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.Models.IsolatedStorage.ExtensionMethods;

namespace Schema.Models.IsolatedStorage
{
    public class GlobalDataModel : IGlobalDataModel
    {
        private string ConnectionsPath = "connections.json";
        public void LoadConnections()
        {
            GlobalData.ConnectionInfos.Clear();


            using (
                IsolatedStorageFile isoStore =
                    IsolatedStorageFile.GetStore(
                        IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly,
                        null, null))
            {
                var files = isoStore.GetFileNames();

                var json = "";
                if (isoStore.FileExists(ConnectionsPath))
                {
                    json = isoStore.ReadAllText(ConnectionsPath);

                }

                if (string.IsNullOrWhiteSpace(json)) return;
                try
                {
                    var storedConnections = JsonConvert.DeserializeObject<ObservableCollection<DatabaseConnectionInfo>>(json.Trim());
                    foreach (var con in storedConnections)
                    {
                        GlobalData.ConnectionInfos.Add(con);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }


        }
    }

}
