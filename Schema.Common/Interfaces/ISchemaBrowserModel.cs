using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
 public interface ISchemaBrowserModel
    {
        void LoadSchemaObjects(ICollection<SchemaObject> objectList, DatabaseConnectionInfo connectionInfo);
    }
}
