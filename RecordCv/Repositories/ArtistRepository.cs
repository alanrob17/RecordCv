using Dapper;
using RecordCv.Data;
using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ArtistRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<Artist>(
                "SELECT * FROM Artist ORDER BY ArtistId");
        }
    }
}
