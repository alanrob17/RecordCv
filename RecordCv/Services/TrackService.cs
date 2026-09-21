using RecordCv.Helpers;
using RecordCv.Models;
using RecordCv.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecordCv.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;

        public TrackService(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GenerateInsertsAsync()
        {
            var tracks = await _trackRepository.GetAllAsync();
            var inserts = new List<string>();

            foreach (var track in tracks)
            {
                var name = SanitiseNullable(track.Name);
                int? trackLength = track.TrackLength == 0 ? null : track.TrackLength;
                var extended = SanitiseNullable(track.Extended);

                inserts.Add(BuildInsert(
                    track.TrackId,
                    track.DiscId,
                    track.TrackNo,
                    name,
                    trackLength,
                    extended));
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

        private static string BuildInsert(
            int trackId,
            int discId,
            int trackNo,
            string? name,
            int? trackLength,
            string? extended)
        {
            var nameSql = name is null ? "NULL" : $"'{name}'";
            var trackLengthSql = trackLength is null ? "NULL" : trackLength.ToString();
            var extendedSql = extended is null ? "NULL" : $"'{extended}'";

            return $"INSERT INTO Track (TrackId, DiscId, TrackNo, Name, TrackLength, Extended) " +
                   $"VALUES ({trackId}, {discId}, {trackNo}, {nameSql}, {trackLengthSql}, {extendedSql})\nGO";
        }
    }
}
