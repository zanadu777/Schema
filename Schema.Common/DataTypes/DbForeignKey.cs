using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Schema.Common.DataTypes
{
    [DebuggerVisualizer(typeof(DbForeignKey))]
    [Serializable]
    public  class DbForeignKey
    {
      public DbForeignKey()
      {
          Columns = new List<DbForeignKeyColumn>();
      }
        public string ConstraintFullName { get; set; }

        public string ForeignKeyTable { get; set; }
        public string PrimaryKeyTable { get; set; }

        public List<DbForeignKeyColumn > Columns { get; set; }

        public List<string> PrimaryKeyColumns
        {
            get
            {
                var cols = from c in Columns
                    select c.PrimaryKeyColumn;
                return cols.ToList();
            }
        }

        public List<string> ForeignKeyColumns {
            get
            {
                var cols = from c in Columns
                           select c.ForeignKeyColumn;
                return cols.ToList();
            }

        }
    }
}
