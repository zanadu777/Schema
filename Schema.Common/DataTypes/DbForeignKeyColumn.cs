using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
    [Serializable]
    public class DbForeignKeyColumn
    {
        public string ForeignKeyColumn { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public int Ordinal { get; set; }
    }
}
