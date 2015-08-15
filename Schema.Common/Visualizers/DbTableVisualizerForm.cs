using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schema.Common.DataTypes;

namespace Schema.Common.Visualizers
{
    public partial class DbTableVisualizerForm : Form
    {
        private DbTable table;

        public DbTableVisualizerForm()
        {
            InitializeComponent();
        }

        public DbTable Table
        {
            get { return table; }
            set
            {
                table = value;
                this.Text = "Visualization for Table " + Table.Name;
                lblName.Text = table.Name;
                this.dataGridView.DataSource = table.ColumnDataTable();
            }
        }
    }
}
