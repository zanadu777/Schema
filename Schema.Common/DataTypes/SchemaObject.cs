using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
    public class SchemaObject
    {
        public string Name { get; set; }

        public string SchemaName { get; set; }

        public string Definition { get; set; }

        public ESchemaObjectType SchemaObjectType { get; set; }
    }
}
