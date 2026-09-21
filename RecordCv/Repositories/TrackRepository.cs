using Dapper;
using RecordCv.Data;
using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TrackRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Track>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<Track>("SELECT * FROM Track ORDER BY TrackId");
        }
    }
}
