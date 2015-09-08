using System.Threading.Tasks;

namespace Schema.Common.Interfaces
{
    public interface IConnectivityTester
    {
        bool TestConnectivity(string connectionString);
        Task<bool> TestConnectivityAsync(string connectionString);
    }
}
