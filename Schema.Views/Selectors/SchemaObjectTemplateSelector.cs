using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Schema.Common.DataTypes;

namespace Schema.Views.Selectors
{
    public class SchemaObjectTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TableTemplate { get; set; }

        public DataTemplate ViewTemplate { get; set; }

        public DataTemplate StoredProcTemplate { get; set; }
        public DataTemplate ObjectTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            var schemaObject = (SchemaObject)item;
            if (schemaObject == null)
                return ObjectTemplate;

            if (schemaObject.SchemaObjectType == ESchemaObjectType.Table)
            {
                return TableTemplate;
            }
            if (schemaObject.SchemaObjectType == ESchemaObjectType.StoredProcedure)
            {
                return StoredProcTemplate;
            }

            if (schemaObject.SchemaObjectType == ESchemaObjectType.View)
            {
                return ViewTemplate;
            }
            return ObjectTemplate;
        }
    }

}
