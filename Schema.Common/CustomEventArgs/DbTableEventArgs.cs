using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.CustomEventArgs
{
  public  class DbTableEventArgs:EventArgs 
    {
        public DbTable Table { get; private set; }

        public DbTableEventArgs(DbTable dbTable)
        {
            Table = dbTable;
        }
    }
}
