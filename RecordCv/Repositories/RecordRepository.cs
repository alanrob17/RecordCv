using Dapper;
using RecordCv.Data;
using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RecordRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<Record>(
                "SELECT * FROM Record ORDER BY RecordId");
        }
    }
}
