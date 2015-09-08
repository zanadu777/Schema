using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.CustomEventArgs
{
  public  class SchemaObjectEventArgs: EventArgs 
    {
        public SchemaObject SchemaObject { get; private set; }

        public SchemaObjectEventArgs(SchemaObject schemaObject)
        {
            SchemaObject = schemaObject;
        }



        public static implicit operator SchemaObjectEventArgs(SchemaObject schemaObject)
        {
            return new SchemaObjectEventArgs(schemaObject);
        }


    }


}
