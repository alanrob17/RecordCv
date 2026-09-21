using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordCv.Data;
using RecordCv.Repositories;
using RecordCv.Services;

namespace RecordCv
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(async (context, services) =>
            {
                // Data layer
                services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

                // Repositories
                services.AddScoped<IArtistRepository, ArtistRepository>();
                services.AddScoped<IRecordRepository, RecordRepository>();
                services.AddScoped<IDiscRepository, DiscRepository>();
                //services.AddScoped<ITrackRepository, TrackRepository>();

                // Services
                services.AddScoped<IArtistService, ArtistService>();
                services.AddScoped<IRecordService, RecordService>();
                services.AddScoped<IDiscService, DiscService>();
                //services.AddScoped<ITrackService, TrackService>();
            });

                var host = builder.Build();

            // -----------------------------------------------------------------------
            // Artist — generate INSERT scripts via the Artist service
            // -----------------------------------------------------------------------
            //var artistService = host.Services.GetRequiredService<IArtistService>();
            //var artistInserts = await artistService.GenerateInsertsAsync();

            //const string artistSqlFile = "Artist.sql";
            //await File.WriteAllLinesAsync(artistSqlFile, artistInserts);
            //Console.WriteLine($"Artist INSERT statements written to {Path.GetFullPath(artistSqlFile)}");

            // -----------------------------------------------------------------------
            // Record — generate INSERT scripts via the Record service
            // -----------------------------------------------------------------------
            //var recordService = host.Services.GetRequiredService<IRecordService>();
            //var recordInserts = await recordService.GenerateInsertsAsync();

            //const string recordSqlFile = "Record.sql";
            //await File.WriteAllLinesAsync(recordSqlFile, recordInserts);
            //Console.WriteLine($"Record INSERT statements written to {Path.GetFullPath(recordSqlFile)}");

            // -----------------------------------------------------------------------
            // Disc — generate INSERT scripts via the Disc service
            // -----------------------------------------------------------------------
            var discService = host.Services.GetRequiredService<IDiscService>();
            var discInserts = await discService.GenerateInsertsAsync();

            const string discSqlFile = "Disc.sql";
            await File.WriteAllLinesAsync(discSqlFile, discInserts);
            Console.WriteLine($"Disc INSERT statements written to {Path.GetFullPath(discSqlFile)}");
        }
    }
}
