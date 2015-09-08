namespace Schema.Common.DataTypes
{
   public enum EKeyStatus
    {
        None,
        PrimaryKey,
        ForeignKey,
        PrimaryAndForeignKey,
        ReferencedPrimaryKey,
        ReferencedPrimaryAndForeignKey
    }
}
