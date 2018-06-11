using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Errata.Text;
using Microsoft.Practices.Prism.Commands;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.ViewModels.Annotations;

namespace Schema.ViewModels
{
    public class GenerateTableSqlVm:IGenerateTableSqlVm ,INotifyPropertyChanged
    {
        private DbTable table;
        private string genratedSql;

        public GenerateTableSqlVm()
        {
            GenerateSqlCommand = new DelegateCommand(GenerateSql);
        }

        private void GenerateSql()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Create Procedure Insert" + table.Name);
            sb.Append("(");
            var chosenColumns = (from col in table.Columns
                where col.IsIdentity == false
                select col).ToList();

            var columNames = (from col in chosenColumns
                select col.Name).ToList();
            foreach (var column in chosenColumns)
            {
                sb.AppendLine("@" + column.Name +  " " + column.DisplayDataType + ",");
            }
            sb.RemoveRight(3);
            sb.AppendLine(")");
            sb.AppendLine("as");
            sb.AppendFormat("Insert  {0}\n", table.Name);
            sb.AppendFormat( "({0})\n", string.Join(",", columNames));
            sb.AppendLine("values");
            sb.Append("(");
            foreach (var column in chosenColumns)
            {
                sb.Append("@" + column.Name + ",");
            }
            sb.RemoveRight(1);
            sb.Append(")\n");
            GenratedSql = sb.ToString();
        }

        public DbTable Table
        {
            get { return table; }
            set
            {
                table = value;
                Columns = table.ColumnDataTable();
            }
        }

       

        public DataTable Columns { get; set; }
        public ICommand GenerateSqlCommand { get; set; }

        public string GenratedSql
        {
            get { return genratedSql; }
            set
            {
                genratedSql = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
