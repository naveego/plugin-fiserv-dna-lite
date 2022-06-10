using System.Data;
using System.Threading.Tasks;

namespace PluginOracleNet.API.Factory
{
    public interface IConnection
    {
        Task OpenAsync();
        Task CloseAsync();
        ITransaction BeginTransaction();
        Task<bool> PingAsync();
        IDbConnection GetConnection();
    }
}