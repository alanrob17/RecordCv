using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Data
{
    /// <summary>
    /// Creates and opens SQL connections using the connection string from application configuration.
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RecordDb")
                ?? throw new InvalidOperationException(
                    "Connection string 'RecordDb' is not configured in appsettings.json.");
        }

        /// <inheritdoc/>
        public async Task<SqlConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
