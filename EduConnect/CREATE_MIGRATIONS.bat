@echo off
echo ========================================
echo Creating EF Core Migration
echo ========================================
echo.
echo IMPORTANT: Make sure the application is STOPPED before running this script!
echo.
pause

cd EduConnect\EduConnect

echo Creating migration...
dotnet ef migrations add InitialCreate --project ..\DAL\DAL.csproj --startup-project . --context ApplicationDbContext

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Migration created successfully!
    echo ========================================
    echo.
    echo Now applying migration to database...
    echo.
    dotnet ef database update --project ..\DAL\DAL.csproj --startup-project . --context ApplicationDbContext
    
    if %ERRORLEVEL% EQU 0 (
        echo.
        echo ========================================
        echo Migration applied successfully!
        echo ========================================
        echo.
        echo Tables should now be created in PostgreSQL database 'academiceye'
        echo You can now start your application.
    ) else (
        echo.
        echo ========================================
        echo ERROR: Failed to apply migration
        echo ========================================
        echo.
        echo Please check:
        echo 1. PostgreSQL is running
        echo 2. Database 'academiceye' exists
        echo 3. Connection string in appsettings.json is correct
    )
) else (
    echo.
    echo ========================================
    echo ERROR: Failed to create migration
    echo ========================================
    echo.
    echo Please check:
    echo 1. Application is stopped
    echo 2. All code compiles without errors
    echo 3. EF Core packages are installed
)

pause

