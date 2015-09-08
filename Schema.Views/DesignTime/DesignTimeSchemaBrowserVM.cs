using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Schema.Common.CustomEventArgs;
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
        public ICommand ShowCodeGenerationWindowCommand { get; }
        public event EventHandler<SchemaObjectEventArgs> OnShowCodeGenerationWindow;

        public ICommand ShowConnectionManagerWindowCommand { get; set; }

        public event System.EventHandler<Common.CustomEventArgs.DatabaseConnectionInfoEventArgs> OnShowConnectionManagerWindow;
        public ICommand GenerateDataAccessCodeCommand { get; set; }
        public ICommand ShowGenerateTableSqlWindowCommand { get; set; }
        public event EventHandler<DbTableEventArgs> OnShowGenerateTableSqlWindow;
        public ICommand GeneratCodeCommand { get; set; }
        public string GeneratedText { get; set; }
        public ICommand GetJsonCommand { get; set; }
    }
}
