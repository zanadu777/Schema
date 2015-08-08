using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.DataTypes
{
    public class DbParameter
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }

    }
}