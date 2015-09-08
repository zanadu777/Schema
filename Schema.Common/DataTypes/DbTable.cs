using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using Schema.Common.Visualizers;

namespace Schema.Common.DataTypes
{
    [DebuggerVisualizer(typeof(DbTableVisualizer))]
    [Serializable]
    public class DbTable : SchemaObject
    {
        public DbTable()
        {
            SchemaObjectType = ESchemaObjectType.Table;
            Columns = new ObservableCollection<DbColumn>();
        }
        public ObservableCollection<DbColumn> Columns { get; set; }

        public DataTable ColumnDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof (string)));
            dt.Columns.Add(new DataColumn("DataType", typeof (string)));
            dt.Columns.Add(new DataColumn("Ordinal", typeof(int)));
            dt.Columns.Add(new DataColumn("MaxLength", typeof(int)));
            dt.Columns.Add(new DataColumn("IsIdentity", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsNullable", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsInPrimaryKey", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsForeignKey", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsReferencedPrimaryKey", typeof(bool)));
            dt.Columns.Add(new DataColumn("DisplayDataType", typeof(string)));

            foreach (var column in Columns)
            {
             var row = dt.NewRow();

                row["Name"] = column.Name;
                row["DataType"] = column.DataType;
                row["Ordinal"] = column.Ordinal;
                row["MaxLength"] = column.MaxLength;
                row["IsIdentity"] = column.IsIdentity;
                row["IsNullable"] = column.IsNullable;
                row["IsInPrimaryKey"] = column.IsInPrimaryKey;
                row["IsForeignKey"] = column.IsForeignKey;
                row["IsReferencedPrimaryKey"] = column.IsReferencedPrimaryKey;
                row["DisplayDataType"] = column.DisplayDataType ;
                dt.Rows.Add(row);
            }
            return dt;
        }

    }
}
