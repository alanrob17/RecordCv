using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Data
{
    /// <summary>
    /// Abstracts the creation of SQL database connections.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates and opens a new <see cref="SqlConnection"/>.
        /// </summary>
        Task<SqlConnection> CreateConnectionAsync();
    }
}
