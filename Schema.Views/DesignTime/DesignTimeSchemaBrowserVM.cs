using System.Collections.ObjectModel;
using System.Windows.Input;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Views.DesignTime
{
    public class DesignTimeSchemaBrowserVM : ISchemaBrowserVM
    {


        readonly ObservableCollection<SchemaObject> schemaObjects = new ObservableCollection<SchemaObject>();
        public ObservableCollection<SchemaObject> SchemaObjects
        {
            get { return schemaObjects; }
        }


        public DatabaseConnectionInfo CurrentDatabase { get; set; }


        public ICommand LoadSchemaCommand { get; set; }

        public ICommand LoadConnectionInfosCommand { get; set; }


        public ICommand ShowQueryWindowCommand { get; set; }



        public event System.EventHandler<Common.CustomEventArgs.DatabaseConnectionInfoEventArgs> OnShowQueryWindow;


        public ICommand ShowConnectionManagerWindowCommand { get; set; }

        public event System.EventHandler<Common.CustomEventArgs.DatabaseConnectionInfoEventArgs> OnShowConnectionManagerWindow;
    }
}
