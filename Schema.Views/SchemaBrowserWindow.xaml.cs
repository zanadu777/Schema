using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Schema.Common.Interfaces;

namespace Schema.Views
{
    /// <summary>
    /// Interaction logic for SchemaBrowserWindow.xaml
    /// </summary>
    public partial class SchemaBrowserWindow : Window,ISchemaBrowserWindow
    {
        private ISchemaBrowserVM viewModel;
        public SchemaBrowserWindow(ISchemaBrowserVM iSchemaBrowserVM)
        {
            viewModel = iSchemaBrowserVM;
            DataContext = viewModel;
            InitializeComponent();
            ;
        }

        private void lstObjects_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Debug.WriteLine("HI");
                ////select the item under the mouse pointer
                //lstObjects.SelectedIndex = lstObjects.IndexFromPoint(e.Location);
                //if (listBox1.SelectedIndex != -1)
                //{
                //    listboxContextMenu.Show();
                //}
            }

        }

        private void lstObjects_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
//MouseEventArgs myMEArs = new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta);
//int selectedIndx = this.listBox1.IndexFromPoint(new Point(e.X, e.Y));
//                if (selectedIndx != ListBox.NoMatches)
//                {
//                    listBox1.SelectedIndex = selectedIndx;
//                }
