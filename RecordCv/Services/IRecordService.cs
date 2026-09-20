using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public interface IRecordService
    {
        /// <summary>
        /// Generates a collection of SQL INSERT statements for all records.
        /// </summary>
        /// <returns>An ordered sequence of SQL INSERT strings, one per record.</returns>
        Task<IEnumerable<string>> GenerateInsertsAsync();
    }
}
