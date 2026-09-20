using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public interface IArtistService
    {
        /// <summary>
        /// Generates a collection of SQL INSERT statements for all artists.
        /// </summary>
        /// <returns>An ordered sequence of SQL INSERT strings, one per artist.</returns>
        Task<IEnumerable<string>> GenerateInsertsAsync();
    }
}
