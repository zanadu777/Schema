using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Schema.Common.CustomEventArgs;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface ISchemaBrowserVM
    {
        ObservableCollection<SchemaObject> SchemaObjects { get; }

        DatabaseConnectionInfo CurrentDatabase { get; }

        ICommand LoadSchemaCommand { get; }

        ICommand LoadConnectionInfosCommand { get; }

        ICommand ShowQueryWindowCommand { get; }

        event EventHandler<DatabaseConnectionInfoEventArgs>  OnShowQueryWindow;


        ICommand ShowCodeGenerationWindowCommand { get; }

        event EventHandler<SchemaObjectEventArgs> OnShowCodeGenerationWindow;

        ICommand ShowConnectionManagerWindowCommand { get; }

        event EventHandler<DatabaseConnectionInfoEventArgs> OnShowConnectionManagerWindow;



        ICommand GenerateDataAccessCodeCommand { get; set; }

        ICommand ShowGenerateTableSqlWindowCommand { get; set; }

        event EventHandler<DbTableEventArgs> OnShowGenerateTableSqlWindow;

       


        ICommand ShowGenerateStoredProcWindowCommand { get; set; }

        event EventHandler<DbStoredProcEventArgs> OnShowGenerateStoredProcWindow;


        ICommand GeneratCodeCommand { get; set; }

        string GeneratedText { get; set; }

        ICommand GetJsonCommand { get; set; }
    }
}
