using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;
using Schema.Common.DataTypes.CodeGeneration;

namespace Schema.Common.Interfaces
{
    public interface  ICodeGenerator
    {

 
        string Generate(CodeGenerationKey key, SchemaObject schemaObject);
    }
}
