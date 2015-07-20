using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Common.Interfaces
{
    public interface IConnectivityTester
    {
        bool TestConnectivity(string connectionString);
        Task<bool> TestConnectivityAsync(string connectionString);
    }
}
