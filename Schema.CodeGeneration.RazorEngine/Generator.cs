using System;
 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating;
using Schema.Common.DataTypes;
using Schema.Common.DataTypes.CodeGeneration;
using Schema.Common.Interfaces;

namespace Schema.CodeGeneration.RazorEngine
{
    public class Generator:ICodeGenerator
    {
 
        public string Generate(CodeGenerationKey key, SchemaObject schemaObject)
        {
            string template = @"@model Schema.Common.DataTypes.DbTable
@{
    Layout = null;
}
Create Procedure Insert@(Model.Name)
@foreach (var col in Model.Columns)
{
@(col.Name)    @col.DisplayDataType  <br />}
as
insert Table @Model.Name";
            var result =
                Engine.Razor.RunCompile(template, "templateKey", null, schemaObject);
             
            return result.Replace("<br />", "\n" ).Replace("&nbsp;" , " ");
        }
    }
}
