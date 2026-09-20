using RecordCv.Helpers;
using RecordCv.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GenerateInsertsAsync()
        {
            var records = await _recordRepository.GetAllAsync();
            var inserts = new List<string>();

            foreach (var record in records)
            {
                // Sanitise required string fields
                var name = SanitiseNullable(record.Name);

                // Sanitise optional string fields
                var field = SanitiseNullable(record.Field);
                var label = SanitiseNullable(record.Label);
                var pressing = SanitiseNullable(record.Pressing);
                var rating = SanitiseNullable(record.Rating);
                var media = SanitiseNullable(record.Media);
                var coverName = SanitiseNullable(record.CoverName);

                // Review may contain newlines — strip them like Biography in ArtistService
                string? review = null;
                if (!string.IsNullOrEmpty(record.Review))
                {
                    var sanitised = StringSanitiser.SanitiseMultiLine(record.Review);
                    review = sanitised.Length <= 28000 ? sanitised : null;
                }

                inserts.Add(BuildInsert(
                    record.RecordId,
                    record.ArtistId,
                    name,
                    field,
                    record.Recorded,
                    label,
                    pressing,
                    rating,
                    record.Discs,
                    media,
                    record.Bought,
                    record.Cost,
                    coverName,
                    review));
            }

            return inserts;
        }

        // -----------------------------------------------------------------------
        // Private helpers
        // -----------------------------------------------------------------------

        /// <summary>
        /// Returns NULL for null/empty strings, or a sanitised value for use in SQL.
        /// </summary>
        private static string? SanitiseNullable(string? value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return StringSanitiser.ReplaceChar(value);
        }

        private static string BuildInsert(
            int recordId,
            int artistId,
            string? name,
            string? field,
            int? recorded,
            string? label,
            string? pressing,
            string? rating,
            int? discs,
            string? media,
            DateTime? bought,
            decimal? cost,
            string? coverName,
            string? review)
        {
            var nameSql = name is null ? "NULL" : $"'{name}'";
            var fieldSql = field is null ? "NULL" : $"'{field}'";
            var recordedSql = recorded is null ? "NULL" : recorded.ToString();
            var labelSql = label is null ? "NULL" : $"'{label}'";
            var pressingSql = pressing is null ? "NULL" : $"'{pressing}'";
            string? discsSql = discs is null ? "NULL" : $"{discs}";
            var mediaSql = media is null ? "NULL" : $"'{media}'";
            var boughtSql = bought is null ? "NULL" : $"'{bought:yyyy-MM-dd}'";
            var costSql = cost?.ToString("F2") ?? "NULL";
            var coverNameSql = coverName is null ? "NULL" : $"'{coverName}'";
            var reviewSql = review is null ? "NULL" : $"'{review}'";
            var ratingSql = rating is null ? "NULL" : $"'{rating}'";

            return $"INSERT INTO Record (RecordId, ArtistId, Name, Field, Recorded, Label, Pressing, Rating, Discs, Media, Bought, Cost, CoverName, Review) " +
                   $"VALUES ({recordId}, {artistId}, {nameSql}, {fieldSql}, {recordedSql}, {labelSql}, {pressingSql}, {ratingSql}, {discsSql}, {mediaSql}, {boughtSql}, {costSql}, {coverNameSql}, {reviewSql})\nGO";
        }
    }
}
