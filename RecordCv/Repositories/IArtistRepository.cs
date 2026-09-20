using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public interface IArtistRepository
    {
        /// <summary>
        /// Retrieves all artists from the database, ordered by ArtistId.
        /// </summary>
        Task<IEnumerable<Artist>> GetAllAsync();
    }
}
