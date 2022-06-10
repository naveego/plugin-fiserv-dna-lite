using Oracle.ManagedDataAccess.Client;

namespace PluginOracleNet.API.Factory
{
    public class Transaction : ITransaction
    {
        private OracleTransaction _txn;

        public Transaction(OracleTransaction txn)
        {
            _txn = txn;
        }
        
        public void Commit()
        {
            _txn.Commit();
        }

        public void Rollback()
        {
            _txn.Rollback();
        }
    }
}