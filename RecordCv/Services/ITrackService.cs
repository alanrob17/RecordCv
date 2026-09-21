using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public interface ITrackService
    {
        /// <summary>
        /// Generates a collection of SQL INSERT statements for all tracks.
        /// </summary>
        /// <returns>An ordered sequence of SQL INSERT strings, one per track.</returns>
        Task<IEnumerable<string>> GenerateInsertsAsync();
    }
}
