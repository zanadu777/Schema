using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
  public  class DbForeignKey
    {
      public DbForeignKey()
      {
          Columns=new Dictionary<string, string>();
      }
        public string ConstraintFullName { get; set; }

        public string ParentTableFullName { get; set; }
        public string ReferencedTableFullName { get; set; }

        public Dictionary<string, string> Columns { get; set; } 
    }
}
