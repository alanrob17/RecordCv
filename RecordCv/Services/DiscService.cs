using RecordCv.Helpers;
using RecordCv.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public class DiscService : IDiscService
    {
        private readonly IDiscRepository _discRepository;

        public DiscService(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GenerateInsertsAsync()
        {
            var discs = await _discRepository.GetAllAsync();
            var inserts = new List<string>();

            foreach (var disc in discs)
            {
                int? freeDbDiscId = disc.FreeDbDiscId == 0 ? null : disc.FreeDbDiscId;
                var freeDbId = SanitiseNullable(disc.FreeDbId);
                int? length = disc.Length == 0 ? null : disc.Length;

                inserts.Add(BuildInsert(
                    disc.DiscId,
                    disc.RecordId,
                    disc.DiscNo,
                    freeDbDiscId,
                    freeDbId,
                    length));
            }

            return inserts;
        }

        // -----------------------------------------------------------------------
        // Private helpers
        // -----------------------------------------------------------------------

        private static string? SanitiseNullable(string? value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return StringSanitiser.ReplaceChar(value);
        }

        private static string BuildInsert(int DiscId, int RecordId, int DiscNo, int? FreeDbDiscId, string? FreeDbId, int? Length)
        {
            var insertQuery = string.Empty;
            var freeDbDiscIdSql = FreeDbDiscId is null ? "NULL" : FreeDbDiscId.ToString();
            var freeDbIdSql = FreeDbId is null ? "NULL" : $"{FreeDbId}";
            var discNoSql = DiscNo.ToString();
            var lengthSql = Length is null ? "NULL" : Length.ToString();

            if (FreeDbId is not null)
            {
            insertQuery = $"INSERT INTO Disc (DiscId, RecordId, DiscNo, FreeDbDiscId, FreeDbId, Length) " +
                   $"VALUES ({DiscId}, {RecordId}, {discNoSql}, {freeDbDiscIdSql}, '{freeDbIdSql}', {lengthSql})\nGO";
            }
            else
            {
                insertQuery = $"INSERT INTO Disc (DiscId, RecordId, DiscNo, FreeDbDiscId, FreeDbId, Length) " +
                       $"VALUES ({DiscId}, {RecordId}, {discNoSql}, {freeDbDiscIdSql}, {freeDbIdSql}, {lengthSql})\nGO";
            }

            return insertQuery;
        }
    }
}
