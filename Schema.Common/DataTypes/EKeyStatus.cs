using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
