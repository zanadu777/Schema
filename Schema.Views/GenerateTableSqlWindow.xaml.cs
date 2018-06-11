using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Schema.Common.Interfaces;

namespace Schema.Views
{
    /// <summary>
    /// Interaction logic for GenerateTableSqlWindow.xaml
    /// </summary>
    public partial class GenerateTableSqlWindow : Window,IGenerateTableSqlWindow
    {
        private IGenerateTableSqlVm viewModel;
        public GenerateTableSqlWindow(IGenerateTableSqlVm iGenerateTableSqlVm)
        {
            viewModel = iGenerateTableSqlVm;
            this.DataContext = viewModel;
            InitializeComponent();
        }

    
    }
}
