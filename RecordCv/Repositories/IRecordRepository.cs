using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public interface IRecordRepository
    {
        /// <summary>
        /// Retrieves all records from the database, ordered by RecordId.
        /// </summary>
        Task<IEnumerable<Record>> GetAllAsync();
    }
}
