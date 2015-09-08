using System.Collections.ObjectModel;

namespace Schema.Common.DataTypes
{
    public class DbStoredProc : SchemaObject
    {
        public DbStoredProc()
        {
            SchemaObjectType = ESchemaObjectType.StoredProcedure;
            Parameters = new ObservableCollection<DbParameter>();
        }



        public ObservableCollection<DbParameter> Parameters { get; set; }
    }
}
