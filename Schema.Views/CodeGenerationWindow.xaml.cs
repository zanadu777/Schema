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
    /// Interaction logic for CodeGenerationWindow.xaml
    /// </summary>
    public partial class CodeGenerationWindow : Window,ICodeGenerationWindow
    {
        private ICodeGenerationVm viewModel;
        public CodeGenerationWindow(ICodeGenerationVm iCodeGenerationVm)
        {  viewModel = iCodeGenerationVm;
            DataContext = viewModel;
            InitializeComponent();
          

        }
    }
}
