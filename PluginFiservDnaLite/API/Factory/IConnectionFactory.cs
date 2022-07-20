using PluginFiservDnaLite.Helper;

namespace PluginFiservDnaLite.API.Factory
{
    public interface IConnectionFactory
    {
        void Initialize(Settings settings);
        IConnection GetConnection();
        ICommand GetCommand(string commandText, IConnection conn);
    }
}