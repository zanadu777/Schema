using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common
{
    public static class DatabaseTypes
    {
        static DatabaseTypes()
        {
            SupportedTypes = new List<string>();
            SupportedTypes.Add("Sql Server");
        }
        public static List<string> SupportedTypes { get; private set; }


    }
}
