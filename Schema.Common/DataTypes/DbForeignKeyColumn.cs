using System;

namespace Schema.Common.DataTypes
{
    [Serializable]
    public class DbForeignKeyColumn
    {
        public string ForeignKeyColumn { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public int Ordinal { get; set; }
    }
}
