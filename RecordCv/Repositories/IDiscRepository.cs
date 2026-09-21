using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public interface IDiscRepository
    {
        /// <summary>
        /// Retrieves all discs from the database, ordered by DiscId.
        /// </summary>
        Task<IEnumerable<Disc>> GetAllAsync();
    }
}
