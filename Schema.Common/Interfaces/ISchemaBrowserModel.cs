using System.Collections.Generic;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
 public interface ISchemaBrowserModel
    {
        void LoadSchemaObjects(ICollection<SchemaObject> objectList, DatabaseConnectionInfo connectionInfo);
    }
}
