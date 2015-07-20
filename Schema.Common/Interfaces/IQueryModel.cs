using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
  public  interface IQueryModel
  {
      QueryResult Execute(string query, DatabaseConnectionInfo connectionInfo);
  }
}
