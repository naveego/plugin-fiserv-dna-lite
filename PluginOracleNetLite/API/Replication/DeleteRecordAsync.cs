using System;
using System.Threading.Tasks;
using PluginOracleNetLite.API.Utility;
using PluginOracleNetLite.Helper;
using PluginOracleNetLite.API.Factory;
using PluginOracleNetLite.DataContracts;

// --- Sourced from Firebird Plugin version 1.0.0-beta ---

namespace PluginOracleNetLite.API.Replication
{
    public static partial class Replication
    {
        private static readonly string DeleteRecordQuery = @"DELETE FROM {0}.{1}
WHERE {2} = '{3}'";

        public static async Task DeleteRecordAsync(IConnectionFactory connFactory, ReplicationTable table,
            string primaryKeyValue)
        {
            var conn = connFactory.GetConnection();
            
            try
            {
                await conn.OpenAsync();

                var cmd = connFactory.GetCommand(string.Format(DeleteRecordQuery,
                        Utility.Utility.GetSafeName(table.SchemaName.ToAllCaps(), '"'),
                        Utility.Utility.GetSafeName(table.TableName, '"'),
                        Utility.Utility.GetSafeName(table.Columns.Find(c => c.PrimaryKey == true).ColumnName, '"'),
                        primaryKeyValue
                    ),
                    conn);

                // check if table exists
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                await conn.CloseAsync();
            }
        }
    }
}