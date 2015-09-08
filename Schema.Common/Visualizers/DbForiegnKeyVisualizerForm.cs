using System.Data;
using System.Windows.Forms;
using Schema.Common.DataTypes;

namespace Schema.Common.Visualizers
{
    public partial class DbForiegnKeyVisualizerForm : Form
    {
        private DbForeignKey foreignKey;
        public DbForiegnKeyVisualizerForm()
        {
            InitializeComponent();
        }

        public DbForeignKey ForeignKey
        {
            get { return foreignKey; }
            set
            {
                foreignKey = value;

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Primary Key Column", typeof (string)));
                dt.Columns.Add(new DataColumn("Foreign Key Column", typeof(string)));

                
            }
        }
    }
}
