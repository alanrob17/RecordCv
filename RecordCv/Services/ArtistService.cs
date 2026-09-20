using RecordCv.Helpers;
using RecordCv.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GenerateInsertsAsync()
        {
            var artists = await _artistRepository.GetAllAsync();
            var inserts = new List<string>();

            foreach (var artist in artists)
            {
                var lastName = StringSanitiser.ReplaceChar(artist.LastName ?? string.Empty);
                var name = StringSanitiser.ReplaceChar(artist.Name ?? string.Empty);

                // Sanitise nullable FirstName
                string? firstName = null;
                if (!string.IsNullOrEmpty(artist.FirstName))
                {
                    firstName = StringSanitiser.ReplaceChar(artist.FirstName);
                }

                // Sanitise nullable Biography — strip newlines, truncate if too long
                string? biography = null;
                if (!string.IsNullOrEmpty(artist.Biography))
                {
                    var sanitised = StringSanitiser.SanitiseMultiLine(artist.Biography);
                    biography = sanitised.Length <= 28000 ? sanitised : null;
                }

                var sql = BuildInsert(
                    artist.ArtistId,
                    firstName,
                    lastName,
                    name,
                    biography
                    );

                inserts.Add(sql);
            }

            return inserts;
        }

        // -----------------------------------------------------------------------
        // Private helpers
        // -----------------------------------------------------------------------

        private static string BuildInsert(
            int artistId,
            string? firstName,
            string lastName,
            string name,
            string? biography)
        {
            var firstNameSql = firstName is null ? "NULL" : $"'{firstName}'";
            var biographySql = biography is null ? "NULL" : $"'{biography}'";

            return $"INSERT INTO Artist (ArtistId, FirstName, LastName, Name, Biography) " +
                   $"VALUES ({artistId}, {firstNameSql}, '{lastName}', '{name}', {biographySql})\nGO";
        }
    }
}
