using Schema.Common.DataTypes;

namespace Schema.Common.Interfaces
{
  public  interface IQueryModel
  {
      QueryResult Execute(string query, DatabaseConnectionInfo connectionInfo);
  }
}
