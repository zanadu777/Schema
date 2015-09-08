using System;

namespace Schema.Common.DataTypes
{
    [Serializable]
    public class SchemaObject
    {
        public string Name { get; set; }

        public string SchemaName { get; set; }

        public string Definition { get; set; }

        public ESchemaObjectType SchemaObjectType { get; set; }
    }
}
