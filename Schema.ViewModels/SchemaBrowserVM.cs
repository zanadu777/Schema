using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Newtonsoft.Json;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;
using Schema.Common.DataTypes.CodeGeneration;
using Schema.Common.Interfaces;
using Schema.ViewModels.ExtensionMethods;

namespace Schema.ViewModels
{
    public class SchemaBrowserVM : BindableBase, ISchemaBrowserVM
    {
        private ISchemaBrowserModel model;
        private ICodeGenerator codeGenerator;
        private DatabaseConnectionInfo currentDatabase;
        private ICommand showQueryWindowCommand;
        private ICommand loadSchemaCommand;
        private ICommand generateDataAccessCodeCommand;
        private ICommand showConnectionManagerWindowCommand;
        private ICommand showGenerateTableSqlWindowCommand;
        private string generatedText;
        private ICommand getJsonCommand;
        private ICommand showCodeGenerationWindowCommand;

        public SchemaBrowserVM(ISchemaBrowserModel iSchemaBrowserModel, ICodeGenerator iCodeGenerator, DatabaseConnectionInfo databaseConnectionInfo)
        {
            model = iSchemaBrowserModel;
            codeGenerator = iCodeGenerator;
            currentDatabase = databaseConnectionInfo;
            GeneratCodeCommand = new DelegateCommand<object>(GeneratCode);
        }

        private void GeneratCode(object obj)
        {
            var kv = (KeyValuePair<string, object>)obj;
            var key = kv.Key;
            var value = (SchemaObject)kv.Value;
            var result = codeGenerator.Generate(new CodeGenerationKey { Name = key }, value);
            GeneratedText = result;
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

        public ObservableCollection<SchemaObject> SchemaObjects { get; set; } = new ObservableCollection<SchemaObject>();

        public DatabaseConnectionInfo CurrentDatabase
        {
            get { return currentDatabase; }
            set
            {
                SetProperty(ref this.currentDatabase, value);
                this.OnPropertyChanged(() => this.CurrentDatabase);
            }
        }

        public ICommand LoadSchemaCommand
        {
            get { return loadSchemaCommand ?? (loadSchemaCommand = new DelegateCommand(LoadSchema)); }
            set { loadSchemaCommand = value; }
        }

        public ICommand LoadConnectionInfosCommand { get; set; }

        public ICommand ShowQueryWindowCommand
        {
            get { return showQueryWindowCommand ?? (showQueryWindowCommand = new DelegateCommand<DatabaseConnectionInfo>(x => OnShowQueryWindow.Raise(this, new DatabaseConnectionInfoEventArgs(x)))); }
            set { showQueryWindowCommand = value; }
        }

        public event EventHandler<DatabaseConnectionInfoEventArgs> OnShowQueryWindow;

        public ICommand ShowCodeGenerationWindowCommand
        {
            get
            {
                return showCodeGenerationWindowCommand ?? (showCodeGenerationWindowCommand = new DelegateCommand<SchemaObject>(x => OnShowCodeGenerationWindow.Raise(this, x)));
            }
            set { showCodeGenerationWindowCommand = value; }
        }

        public event EventHandler<SchemaObjectEventArgs> OnShowCodeGenerationWindow;

        public ICommand ShowConnectionManagerWindowCommand
        {
            get
            {
                return showConnectionManagerWindowCommand ?? (showConnectionManagerWindowCommand
                  = new DelegateCommand<DatabaseConnectionInfo>(x => OnShowConnectionManagerWindow.Raise(this, new DatabaseConnectionInfoEventArgs(x))));
            }
            set { showConnectionManagerWindowCommand = value; }
        }

        public event EventHandler<DatabaseConnectionInfoEventArgs> OnShowConnectionManagerWindow;

        public ICommand GenerateDataAccessCodeCommand
        {
            get { return generateDataAccessCodeCommand ?? (generateDataAccessCodeCommand = new DelegateCommand<object>(GenerateDataAccessCode)); }
            set { generateDataAccessCodeCommand = value; }
        }

        public ICommand ShowGenerateTableSqlWindowCommand
        {
            get { return showGenerateTableSqlWindowCommand ?? (showGenerateTableSqlWindowCommand = new DelegateCommand<DbTable>(x => OnShowGenerateTableSqlWindow.Raise(this, new DbTableEventArgs(x)))); }
            set { showGenerateTableSqlWindowCommand = value; }
        }

        public event EventHandler<DbTableEventArgs> OnShowGenerateTableSqlWindow;
        public ICommand GeneratCodeCommand { get; set; }

        public string GeneratedText
        {
            get { return generatedText; }
            set
            {
                SetProperty(ref generatedText, value);
                OnPropertyChanged(() => GeneratedText);
            }
        }

        public ICommand GetJsonCommand
        {
            get { return getJsonCommand ?? (getJsonCommand = new DelegateCommand<SchemaObject>(GetJson)); }
            set { getJsonCommand = value; }
        }

        private void GetJson(SchemaObject obj)
        {
            var settigns = new JsonSerializerSettings();
            settigns.Formatting = Formatting.Indented;
            string json = JsonConvert.SerializeObject(obj, settigns);
            GeneratedText = json;
        }
    }
}
