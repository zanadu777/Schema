using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

        ICommand ShowConnectionManagerWindowCommand { get; }

        event EventHandler<DatabaseConnectionInfoEventArgs> OnShowConnectionManagerWindow;



        ICommand GenerateDataAccessCodeCommand { get; set; }

        ICommand ShowGenerateTableSqlWindowCommand { get; set; }

        event EventHandler<DbTableEventArgs> OnShowGenerateTableSqlWindow;
    }
}
