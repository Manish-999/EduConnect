# EF Core Migration Instructions

## Important: Stop the Running Application First!

Before creating or applying migrations, you **MUST** stop the running .NET application (process ID 31400). The application is currently locking DLL files and preventing the build.

### Steps to Complete:

1. **Stop the running application**
   - Press `Ctrl+C` in the terminal where the app is running, OR
   - Close the application window, OR
   - Kill the process: `taskkill /F /PID 31400`

2. **Create the migration** (after stopping the app):
   ```bash
   cd EduConnect\EduConnect
   dotnet ef migrations add InitialCreate --project ..\DAL\DAL.csproj --startup-project . --context ApplicationDbContext
   ```

3. **Apply the migration to database**:
   ```bash
   dotnet ef database update --project ..\DAL\DAL.csproj --startup-project . --context ApplicationDbContext
   ```

4. **Verify the migration**:
   - Check that the `schools` table was created in PostgreSQL
   - Connect to database: `psql -U postgres -d academiceye`
   - Run: `\dt` to see tables
   - Run: `\d schools` to see the table structure

5. **Start the application again**:
   ```bash
   dotnet run
   ```

## What Was Implemented:

✅ **School Entity** (`Model/Entities/School.cs`)
   - Complete EF Core entity with all properties mapped to PostgreSQL columns
   - Uses snake_case column names matching your existing schema

✅ **ApplicationDbContext** (`DAL/ApplicationDbContext.cs`)
   - DbSet<School> configured
   - Proper entity configuration with indexes

✅ **Program.cs Updates**
   - DbContext registered with PostgreSQL connection
   - Connection retry logic configured
   - Database connection test on startup

✅ **SchoolController Updates**
   - SaveSchool now uses EF Core: `_context.Schools.Add()` and `SaveChangesAsync()`
   - GetAllSchool uses EF Core: `_context.Schools.ToListAsync()`
   - Proper error handling

✅ **NuGet Packages Installed**
   - Microsoft.EntityFrameworkCore (8.0.0)
   - Microsoft.EntityFrameworkCore.Design (8.0.0)
   - Npgsql.EntityFrameworkCore.PostgreSQL (8.0.0)

## Connection String

The connection string in `appsettings.json` is:
```
Host=localhost;Port=5432;Database=academiceye;Username=postgres;Password=password;Pooling=true;MinPoolSize=5;MaxPoolSize=20;Connection Lifetime=300;Command Timeout=30;
```

## Troubleshooting

If migration creation fails:
1. Ensure PostgreSQL is running
2. Verify database `academiceye` exists: `CREATE DATABASE academiceye;`
3. Check connection string is correct
4. Ensure all NuGet packages are restored: `dotnet restore`

If database update fails:
1. Check PostgreSQL logs
2. Verify user `postgres` has CREATE TABLE permissions
3. Check if `schools` table already exists (may need to drop it first)

