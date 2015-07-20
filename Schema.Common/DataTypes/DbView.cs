using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
   public class DbView : SchemaObject
    {
        public DbView()
        {
            SchemaObjectType = ESchemaObjectType.View;
            Columns = new ObservableCollection<DbColumn>();
        }
        public ObservableCollection<DbColumn> Columns { get; set; }
    }
}
