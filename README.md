# RecordCv

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C# 14](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![Microsoft SQL Server](https://img.shields.io/badge/SQL%20Server-2025-CC292B?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)

A **.NET 10 console application** that connects to the RecordDB database and generates ready-to-run SQL `INSERT` scripts for each entity — **Artist**, **Disc**, **Record**, and **Track** — writing each set of statements to its own `.sql` output file.

---

## Features

- Queries a SQL Server database using **Dapper** for fast, lightweight data access
- Generates syntactically correct `INSERT` statements with full `NULL` handling for every nullable field
- Sanitises string values — normalises Unicode quotation marks and escapes single quotes for SQL safety
- Strips newlines from multi-line fields (e.g. biographies, reviews) and enforces a maximum field length
- Writes output to four separate `.sql` files, one per entity
- Built on **ASP.NET Core Generic Host** with full dependency injection

---

## Architecture

The application follows a clean **Repository → Service** layered pattern:

```
┌─────────────┐     ┌──────────────────────┐     ┌──────────────────┐
│  Program.cs │────▶│  IXxxService         │────▶│  IXxxRepository  │
│  (DI wiring │     │  GenerateInsertsAsync│     │  GetAllAsync()   │
│  + output)  │     └──────────────────────┘     └──────────────────┘
└─────────────┘              │                            │
                             ▼                            ▼
                     StringSanitiser              IDbConnectionFactory
                     (Helpers layer)              (Data layer / Dapper)
```

| Layer | Responsibility |
|-------|---------------|
| `Data/` | Creates and opens SQL connections via `IDbConnectionFactory` |
| `Repositories/` | Dapper queries — one repository per entity |
| `Services/` | INSERT generation logic — one service per entity |
| `Helpers/` | Shared string sanitisation utilities |
| `Models/` | POCO classes mapped to database tables |

---

## Project Structure

```
RecordCvApp/
├── RecordCv/
│   ├── Data/
│   │   ├── IDbConnectionFactory.cs
│   │   └── DbConnectionFactory.cs
│   ├── Helpers/
│   │   └── StringSanitiser.cs
│   ├── Models/
│   │   ├── Artist.cs
│   │   ├── Disc.cs
│   │   ├── Record.cs
│   │   └── Track.cs
│   ├── Repositories/
│   │   ├── IArtistRepository.cs / ArtistRepository.cs
│   │   ├── IDiscRepository.cs   / DiscRepository.cs
│   │   ├── IRecordRepository.cs / RecordRepository.cs
│   │   └── ITrackRepository.cs  / TrackRepository.cs
│   ├── Services/
│   │   ├── IArtistService.cs / ArtistService.cs
│   │   ├── IDiscService.cs   / DiscService.cs
│   │   ├── IRecordService.cs / RecordService.cs
│   │   └── ITrackService.cs  / TrackService.cs
│   ├── NullableTimeOnlyTypeHandler.cs
│   ├── TimeOnlyTypeHandler.cs
│   ├── appsettings.json
│   ├── RecordCv.csproj
│   └── Program.cs
└── README.md
```

---

## Prerequisites

| Requirement | Version |
|-------------|---------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.0 or later |
| SQL Server | 2019 or later (any edition) |

---

## Configuration

The application reads the database connection string from `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MusicDb": "Server=<your-server>;Initial Catalog=MusicDB;User ID=<user>;Password=<password>;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

To use User Secrets instead:

```bash
dotnet user-secrets init --project RecordCv
dotnet user-secrets set "ConnectionStrings:RecordDb" "Server=...;..." --project RecordCv
```

---

## Running the Application

```bash
cd RecordCv
dotnet run
```

On completion, the console will confirm the location of each output file:

```
Artist INSERT statements written to C:\...\Artist.sql
Disc INSERT statements written to C:\...\Disc.sql
Record INSERT statements written to C:\...\Record.sql
Track INSERT statements written to C:\...\Track.sql
```

### Building a Release Executable

```bash
dotnet publish -c Release -r win-x64 --self-contained
```

---

## Output Files

| File | Contents |
|------|----------|
| `Artist.sql` | `INSERT INTO Artist ...` statements, ordered by `ArtistId` |
| `Disc.sql` | `INSERT INTO Disc ...` statements, ordered by `DiscId` |
| `Record.sql` | `INSERT INTO Record ...` statements, ordered by `RecordId` |
| `Track.sql` | `INSERT INTO Track ...` statements, ordered by `TrackId` |

Each statement is followed by a `GO` batch separator for direct execution in SQL Server Management Studio (SSMS) or `sqlcmd`.

**Example output:**
```sql
INSERT INTO Artist (ArtistId, FirstName, LastName, Name, Biography) VALUES (1, 'Miles', 'Davis', 'Miles Davis', 'Miles Davis was an American...')
GO
```

---

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| [Dapper](https://github.com/DapperLib/Dapper) | 2.1.79 | Micro-ORM for SQL queries |
| [Dapper.Contrib](https://github.com/DapperLib/Dapper.Contrib) | 2.0.78 | Dapper extension methods |
| [Microsoft.Data.SqlClient](https://github.com/dotnet/SqlClient) | 7.0.3 | SQL Server driver |
| Microsoft.Extensions.Hosting | 10.0.x | Generic Host & DI container |
| Microsoft.Extensions.Configuration.Json | 10.0.x | `appsettings.json` support |

---
