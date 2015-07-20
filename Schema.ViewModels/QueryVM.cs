using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.ViewModels
{
    public class QueryVM : BindableBase, IQueryVM
    {
        private IQueryModel model;
        private DatabaseConnectionInfo connectionInfo;
        private string query;
        private QueryResult queryResult;

        private System.Windows.Threading.DispatcherTimer queryTimer;

        private BackgroundWorker queryWorker;
        private TimeSpan queryTime;

        public QueryVM(IQueryModel iQueryModel)
        {
            model = iQueryModel;
            ExecuteQueryCommand = new DelegateCommand<string>(ExecuteQuery);

            queryTimer = new System.Windows.Threading.DispatcherTimer();
            queryTimer.Tick += queryTimer_Tick;
            queryTimer.Interval = new TimeSpan(0, 0, 1);

            queryWorker = new BackgroundWorker();
            queryWorker.DoWork += queryWorker_DoWork;
            queryWorker.RunWorkerCompleted += queryWorker_RunWorkerCompleted;
            queryWorker.WorkerReportsProgress = true;
        }

        private bool isqueryRunning;
        void queryTimer_Tick(object sender, EventArgs e)
        {
            QueryTime = QueryTime.Add(new TimeSpan(0, 0, 1));
        }


        void queryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isqueryRunning = false;
            ((DelegateCommand<string>)ExecuteQueryCommand).RaiseCanExecuteChanged();
            queryTimer.Stop();
            QueryTime = queryResult.QueryTimeSpan;
        }

        private void queryWorker_DoWork(object sender, DoWorkEventArgs e)
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
    }
}
