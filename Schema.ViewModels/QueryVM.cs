using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using Errata.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Win32;
using OfficeOpenXml;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;
using Schema.ViewModels.ExtensionMethods;

namespace Schema.ViewModels
{
    public class QueryVM : BindableBase, IQueryVM
    {
        private IQueryModel model;
        private DatabaseConnectionInfo connectionInfo;
        private string query;
        private QueryResult queryResult;

        private DispatcherTimer queryTimer;

        private BackgroundWorker queryWorker;
        private TimeSpan queryTime;

        public QueryVM(IQueryModel iQueryModel)
        {
            model = iQueryModel;
            ExecuteQueryCommand = new DelegateCommand<string>(ExecuteQuery);
            ExportToExcelCommand = new DelegateCommand(ExportToExcel);
            GenerateSnippetCommand = new DelegateCommand<string>(GenerateSnippet);


            queryTimer = new DispatcherTimer();
            queryTimer.Tick += QueryTimer_Tick;
            queryTimer.Interval = new TimeSpan(0, 0, 1);

            queryWorker = new BackgroundWorker();
            queryWorker.DoWork += QueryWorker_DoWork;
            queryWorker.RunWorkerCompleted += QueryWorker_RunWorkerCompleted;
            queryWorker.WorkerReportsProgress = true;
        }

        private void GenerateSnippet(string obj)
        {
            switch (obj)
            {
                case "DataReaderAccess":
                    StringBuilder sb = new StringBuilder();
                    foreach (DataColumn  column in queryResult.DataTable.Columns)
                    {
                        sb.AppendLine(string.Format("var {0}Pos = reader.GetOrdinal(\"{1}\");", column.ColumnName.FirstToLower(), column.ColumnName ));
                    }
                    GeneratedSnippet += sb.ToString();
                    break;
            }
           
        }

        private void ExportToExcel()
        {

            SaveFileDialog dia = new SaveFileDialog();

            dia.Filter = "Excel Workbook (*.xlsx)|*xlsx|All files (*.*)|*.*";

            var result = dia.ShowDialog();
            if (result != true)
                return;

            var  newFile = new FileInfo(dia.FileName);
            if (newFile.Exists)
                newFile.Delete();

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                var colCounter = 1;
                foreach (DataColumn column in QueryResult.DataTable.Columns)
                {
                    worksheet.Cells[1, colCounter ].Value = column.ColumnName;
                    colCounter ++;
                }
                var colCount = QueryResult.DataTable.Columns.Count;

                var rowCounter = 2;
                foreach (DataRow row in QueryResult.DataTable.Rows)
                {
                    var values = row.ItemArray;
                    for (int iCol = 0; iCol < colCount; iCol++)
                    {
                        worksheet.Cells[rowCounter, iCol+1].Value = values[iCol];
                    }
                    rowCounter ++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
        }

        private bool isqueryRunning;
        private string generatedSnippet;

        void QueryTimer_Tick(object sender, EventArgs e)
        {
            QueryTime = QueryTime.Add(new TimeSpan(0, 0, 1));
        }


        void QueryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isqueryRunning = false;
            ((DelegateCommand<string>)ExecuteQueryCommand).RaiseCanExecuteChanged();
            queryTimer.Stop();
            QueryTime = queryResult.QueryTimeSpan;
        }

        private void QueryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                QueryResult = model.Execute(e.Argument.ToString(), connectionInfo);

            }
            catch (Exception ex)
            {

            }
        }


        private void ExecuteQuery(string obj)
        {
            QueryResult = new QueryResult();
            ((DelegateCommand<string>)ExecuteQueryCommand).RaiseCanExecuteChanged();
            isqueryRunning = true;
            queryTimer.Start();
            queryWorker.RunWorkerAsync(obj);

        }

        public string Query
        {
            get { return query; }
            set
            {
                SetProperty(ref query, value);
                OnPropertyChanged(() => Query);
            }
        }

        public DatabaseConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
            set
            {
                SetProperty(ref connectionInfo, value);
                OnPropertyChanged(() => ConnectionInfo);
            }
        }


        public ICommand ExecuteQueryCommand { get; set; }

        public QueryResult QueryResult
        {
            get { return queryResult; }
            set
            {
                SetProperty(ref queryResult, value);
                OnPropertyChanged(() => QueryResult);
            }
        }

        public TimeSpan QueryTime
        {
            get { return queryTime; }
            set
            {
                SetProperty(ref queryTime, value);
                OnPropertyChanged(() => QueryTime);
            }
        }

        public ICommand ExportToExcelCommand { get; set; }
        public ICommand GenerateSnippetCommand { get; set; }

        public string GeneratedSnippet
        {
            get { return generatedSnippet; }
            set
            {
                SetProperty(ref generatedSnippet, value);
                OnPropertyChanged(() => GeneratedSnippet);
            }
        }
    }
}
