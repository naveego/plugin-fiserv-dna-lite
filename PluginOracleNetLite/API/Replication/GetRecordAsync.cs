using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naveego.Sdk.Logging;
using PluginOracleNetLite.Helper;
using PluginOracleNetLite.API.Utility;
using PluginOracleNetLite.API.Factory;
using PluginOracleNetLite.DataContracts;

// --- Sourced from Firebird Plugin version 1.0.0-beta ---

namespace PluginOracleNetLite.API.Replication
{
    public static partial class Replication
    {
        private static readonly string GetRecordQuery = @"SELECT * FROM {0}.{1}
WHERE {2} = '{3}'";

        public static async Task<Dictionary<string, object>> GetRecordAsync(IConnectionFactory connFactory,
            ReplicationTable table,
            string primaryKeyValue)
        {
            var conn = connFactory.GetConnection();
            
            try
            {
                await conn.OpenAsync();

                var cmd = connFactory.GetCommand(string.Format(GetRecordQuery,
                        Utility.Utility.GetSafeName(table.SchemaName.ToAllCaps(), '"'),
                        Utility.Utility.GetSafeName(table.TableName, '"'),
                        Utility.Utility.GetSafeName(table.Columns.Find(c => c.PrimaryKey == true)?.ColumnName, '"'),
                        primaryKeyValue
                    ),
                    conn);
            
                var reader = await cmd.ExecuteReaderAsync();

                Dictionary<string, object> recordMap = null;
                // check if record exists
                if (reader.HasRows())
                {
                    await reader.ReadAsync();

                    recordMap = new Dictionary<string, object>();

                    foreach (var column in table.Columns)
                    {
                        try
                        {
                            recordMap[column.ColumnName] = reader.GetValueById(column.ColumnName, '"');
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e, $"No column with column name: {column.ColumnName}");
                            Logger.Error(e, e.Message);
                            recordMap[column.ColumnName] = null;
                        }
                    }
                }

                return recordMap;
            }
            finally
            {
                await conn.CloseAsync();
            }
        }
    }
}