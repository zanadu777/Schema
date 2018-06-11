using System.Data;
using Schema.Common.DataTypes;
using Schema.Common.Interfaces;

namespace Schema.Views.DesignTime
{
    public static class DesignTimeData
    {
        public static ISchemaBrowserVM SchemaBrowserVM
        {
            get
            {
                var vm = new DesignTimeSchemaBrowserVM();
                vm.SchemaObjects.Add(new SchemaObject { Name = "User", SchemaObjectType = ESchemaObjectType.Table });
                vm.SchemaObjects.Add(new SchemaObject { Name = "Card", SchemaObjectType = ESchemaObjectType.Table });
                vm.SchemaObjects.Add(new SchemaObject { Name = "View", SchemaObjectType = ESchemaObjectType.View });
                return vm;


            }
        }

        public static IQueryVM QueryVM
        {
            get
            {
                var vm = new DesignTimeQueryVM();
                vm.Query = "Select * from Bitmap";
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("Value", typeof(string)));

                var row = dt.NewRow();
                row[0] = 1;
                row[1] = "Test";

                vm.QueryResult.DataTable = dt;
                return vm;
            }
        }


        public static IConnectionManagerVM ConnectionManagerVM
        {
            get
            {
                var vm = new DesignTimeConnectionManagerVM();
                vm.Connections.Add(new DatabaseConnectionInfo
                {
                    FriendlyName = "Mercury FlashCard",
                    ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
                });
                return vm;
            }
        }


        public static IGenerateTableSqlVm GenerateTableSqlVm
        {
            get
            {
                var vm = new DesignTimeGenerateTableSqlVm();
                var table = new DbTable();
                table.Name = "Objects";
                table.Columns.Add(new DbColumn { Name = "Id", DataType = "int", IsIdentity = true, IsInPrimaryKey = true });
                vm.Table = table;
                return vm;
            }
        }


        public static DbColumn DBColumn
        {
            get { return new DbColumn(); }
        }

        public static DbTable DbTable
        {
            get { return new DbTable(); }
        }

        public static ICodeGenerationVm CodeGenerationVm
        {
            get
            {
                var vm = new DesignTimeCodeGenerationVm();

                var table = new DbTable();
                table.Name = "Objects";
                table.Columns.Add(new DbColumn { Name = "Id", DataType = "int", IsIdentity = true, IsInPrimaryKey = true });

                vm.SchemaObject = table;

                return vm;
            }
        }


        public static IGenerateStoredProcVm GenerateStoredProcVm
        {
            get
            {
               var vm = new DesignTimeGenerateStoredProcVm();
                return vm;
            }
        }
    }
}

