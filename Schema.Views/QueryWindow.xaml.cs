using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using Schema.Common.Interfaces;

namespace Schema.Views
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window, IQueryWindow
    {
        private IQueryVM vm;
        public QueryWindow(IQueryVM iQueryVM)
        {
            vm = iQueryVM;
            DataContext = vm;
            InitializeComponent();

            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(QueryWindow).Assembly.GetManifestResourceStream("Schema.Views.CustomHighlighting.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            // and register it in the HighlightingManager
            HighlightingManager.Instance.RegisterHighlighting("Custom Highlighting", new string[] { ".cool" }, customHighlighting);
            txtQuery.SyntaxHighlighting = customHighlighting;

        }

        private void btExecute_Click(object sender, RoutedEventArgs e)
        {
            string query;
            if (txtQuery.SelectedText.Length > 0)
                query = txtQuery.SelectedText;
            else
                query = txtQuery.Text;
            if (!string.IsNullOrWhiteSpace(query))
                vm.ExecuteQueryCommand.Execute(query);
        }
    }
}
