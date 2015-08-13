using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.ViewModels.ExtensionMethods;

namespace Schema.ViewModels
{
    public class SchemaBrowserVM : BindableBase, ISchemaBrowserVM
    {
        private ISchemaBrowserModel model;
        private DatabaseConnectionInfo currentDatabase;

        public SchemaBrowserVM(ISchemaBrowserModel iSchemaBrowserModel, DatabaseConnectionInfo databaseConnectionInfo)
        {
            model = iSchemaBrowserModel;
            currentDatabase = databaseConnectionInfo;
            SchemaObjects = new ObservableCollection<SchemaObject>();
            LoadSchemaCommand = new DelegateCommand(LoadSchema);

            ShowQueryWindowCommand = new DelegateCommand<DatabaseConnectionInfo>(x => OnShowQueryWindow.Raise(this, new DatabaseConnectionInfoEventArgs(x)));
            ShowConnectionManagerWindowCommand = new DelegateCommand<DatabaseConnectionInfo>(x => OnShowConnectionManagerWindow.Raise(this, new DatabaseConnectionInfoEventArgs(x)));
            ShowGenerateTableSqlWindowCommand = new DelegateCommand<DbTable>(x => OnShowGenerateTableSqlWindow.Raise(this, new DbTableEventArgs(x)));
               
            GenerateDataAccessCodeCommand = new DelegateCommand<object>(GenerateDataAccessCode);
        }

        private void GenerateDataAccessCode(object obj)
        {
            if (obj != null)
            {
                var sp = obj as DbStoredProc;
                Debug.WriteLine(sp.Name);
            }
        }
    

        private void LoadSchema()
        {
            model.LoadSchemaObjects(SchemaObjects, currentDatabase);
            CurrentDatabase = currentDatabase;
        }

        public ObservableCollection<SchemaObject> SchemaObjects { get; set; }

        public DatabaseConnectionInfo CurrentDatabase
        {
            get { return currentDatabase; }
            set
            {
                SetProperty(ref this.currentDatabase, value);
                this.OnPropertyChanged(() => this.CurrentDatabase);
            }
        }

        public ICommand LoadSchemaCommand { get; set; }


        public ICommand LoadConnectionInfosCommand { get; set; }



        public ICommand ShowQueryWindowCommand { get; set; }

        public event EventHandler<DatabaseConnectionInfoEventArgs> OnShowQueryWindow;


        public ICommand ShowConnectionManagerWindowCommand { get; set; }

        public event EventHandler<DatabaseConnectionInfoEventArgs> OnShowConnectionManagerWindow;
        public ICommand GenerateDataAccessCodeCommand { get; set; }
        public ICommand ShowGenerateTableSqlWindowCommand { get; set; }
        public event EventHandler<DbTableEventArgs> OnShowGenerateTableSqlWindow;
    }
}
