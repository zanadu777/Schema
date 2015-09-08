using System.Collections.ObjectModel;

namespace Schema.Common.DataTypes
{
   public class DbView : SchemaObject
    {
        public DbView()
        {
            SchemaObjectType = ESchemaObjectType.View;
            Columns = new ObservableCollection<DbColumn>();
        }
        public ObservableCollection<DbColumn> Columns { get; set; }
    }
}
