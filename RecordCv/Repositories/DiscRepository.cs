using Dapper;
using RecordCv.Data;
using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public class DiscRepository : IDiscRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DiscRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Disc>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<Disc>(
                "SELECT * FROM Disc ORDER BY DiscId");
        }
    }
}
