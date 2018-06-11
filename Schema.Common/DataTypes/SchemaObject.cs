using System;

namespace Schema.Common.DataTypes
{
    [Serializable]
    public class SchemaObject
    {
        public string Name { get; set; }

        public string SchemaName { get; set; }

        /// <summary>
        /// The SQl definition of the object
        /// </summary>
        public string Definition { get; set; }

        public ESchemaObjectType SchemaObjectType { get; set; }
    }
}
