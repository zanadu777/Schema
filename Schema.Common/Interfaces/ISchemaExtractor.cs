using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
    public interface ISchemaExtractor
    {
        Dictionary<string, DbTable> GetTables(DatabaseConnectionInfo connectionInfo);
        Dictionary<string, List<DbColumn>> GetTableColumns(DatabaseConnectionInfo connectionInfo);

        Dictionary<string, DbView> GetViews(DatabaseConnectionInfo connectionInfo);
        Dictionary<string, List<DbColumn>> GetViewColumns(DatabaseConnectionInfo connectionInfo);

        Dictionary<string, DbStoredProc> GetStoredProcedures(DatabaseConnectionInfo connectionInfo);

        Dictionary<string, List<DbParameter>> GetStoredProcedureParameters(DatabaseConnectionInfo connectionInfo);


        string DisplayType(DbColumn column);

    }
}
