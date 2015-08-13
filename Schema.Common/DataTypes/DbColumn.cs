using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
    [Serializable]
    public class DbColumn
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Ordinal { get; set; }

        public int MaxLength { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsNullable { get; set; }

        public bool IsInPrimaryKey { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsReferenced { get; set; }

        public string DisplayDataType
        {
            get
            {
                return DisplayDataTypeCalculator != null ? DisplayDataTypeCalculator(this) : DataType;
            }
        }

        public Func<DbColumn, string> DisplayDataTypeCalculator { get; set; }

       
    }
}
