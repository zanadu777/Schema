using System;

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

        public bool IsReferencedPrimaryKey { get; set; }

        public DbColumn() { }


        public string DisplayDataType { get; set; }

        public EKeyStatus  KeyStatus
        {
            get
            {
                if (IsInPrimaryKey && IsForeignKey && IsReferencedPrimaryKey)
                    return EKeyStatus.ReferencedPrimaryAndForeignKey;

                if (IsInPrimaryKey && IsForeignKey)
                    return EKeyStatus.PrimaryAndForeignKey;

                if (IsInPrimaryKey && IsReferencedPrimaryKey)
                    return EKeyStatus.ReferencedPrimaryKey;

                if (IsInPrimaryKey)
                    return EKeyStatus.PrimaryKey;

                if (IsForeignKey)
                    return EKeyStatus.ForeignKey;

                return EKeyStatus.None;
            }
        }
    }
}
