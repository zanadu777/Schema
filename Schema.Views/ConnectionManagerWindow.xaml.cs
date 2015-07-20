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
    /// Interaction logic for ConnectionManagerWindow.xaml
    /// </summary>
    public partial class ConnectionManagerWindow : Window
    {
        private IConnectionManagerVM viewModel;
        public ConnectionManagerWindow(IConnectionManagerVM iConnectionManagerVM)
        {
            viewModel = iConnectionManagerVM;
            DataContext = viewModel;
            InitializeComponent();
            viewModel.OnConnectionSelected += ViewModel_OnConnectionSelected;
        }

        void ViewModel_OnConnectionSelected(object sender, Common.CustomEventArgs.DatabaseConnectionInfoEventArgs e)
        {
            LstConnections.SelectedItem = e.ConnectionInfo;
        }
    }
}
