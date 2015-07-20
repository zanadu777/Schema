using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
    public class DbStoredProc : SchemaObject
    {
        public DbStoredProc()
        {
            SchemaObjectType = ESchemaObjectType.StoredProcedure;
            Parameters = new ObservableCollection<DbParameter>();
        }



        public ObservableCollection<DbParameter> Parameters { get; set; }
    }
}
