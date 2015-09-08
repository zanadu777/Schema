using System.Collections.Generic;

namespace Schema.Common
{
    public static class DatabaseTypes
    {
        static DatabaseTypes()
        {
            SupportedTypes = new List<string>();
            SupportedTypes.Add("Sql Server");
            SupportedTypes.Add("MySQL");
        }
        public static List<string> SupportedTypes { get; private set; }


    }
}
