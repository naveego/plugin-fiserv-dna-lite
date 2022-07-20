namespace PluginFiservDnaLite.API.Factory
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
    }
}