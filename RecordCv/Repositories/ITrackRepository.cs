using RecordCv.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Repositories
{
    public interface ITrackRepository
    {
        /// <summary>
        /// Retrieves all tracks from the database, ordered by TrackId.
        /// </summary>
        Task<IEnumerable<Track>> GetAllAsync();
    }
}
