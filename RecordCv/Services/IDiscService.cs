using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public interface IDiscService
    {
        /// <summary>
        /// Generates a collection of SQL INSERT statements for all discs.
        /// </summary>
        /// <returns>An ordered sequence of SQL INSERT strings, one per disc.</returns>
        Task<IEnumerable<string>> GenerateInsertsAsync();
    }
}
