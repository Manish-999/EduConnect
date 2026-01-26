using DAL;
using DAL.Interfaces;
using DAL.Methods;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using Npgsql;
using Services.Interfaces;
using Services.Methods;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Convert PascalCase to camelCase for JSON serialization
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors.Select(e => new { Field = x.Key, Message = e.ErrorMessage }))
                .ToList();

            return new BadRequestObjectResult(new ApiResponse<object>
            {
                Success = false,
                Message = "Validation failed",
                Data = errors
            });
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;

    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured"))),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EduConnect",
        Version = "v1"
    });

    // 🔐 JWT Bearer configuration
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your_token_here}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

_ = builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("Jwt"));

// Get PostgreSQL connection string
var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("PostgreSQL connection string is not configured.");
}

// Configure Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    }));

// Register NpgsqlDataSource for Dapper (if still needed)
var dataSource = NpgsqlDataSource.Create(connectionString);
builder.Services.AddSingleton(dataSource);

// Register services
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<ISchoolService, SchoolService>();
builder.Services.AddSingleton<ICommonDAL>(sp => new CommonDAL(sp.GetRequiredService<NpgsqlDataSource>()));
builder.Services.AddSingleton<IInMemorySchoolStore, InMemorySchoolStore>();
var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable static file serving for uploaded files
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Test database connection and ensure tables exist on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        var canConnect = await dbContext.Database.CanConnectAsync();
        Console.WriteLine($"Database connection: {(canConnect ? "SUCCESS" : "FAILED")}");
        
        if (canConnect)
        {
            // Check if migrations have been applied
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                Console.WriteLine($"WARNING: {pendingMigrations.Count()} pending migration(s) detected!");
                Console.WriteLine("Run: dotnet ef database update --project ..\\DAL\\DAL.csproj --startup-project . --context ApplicationDbContext");
            }
            else
            {
                Console.WriteLine("All migrations are applied.");
            }

            // Try to ensure database is created (will create tables if migrations haven't been applied)
            // This is a fallback for development - migrations are preferred
            try
            {
                await dbContext.Database.EnsureCreatedAsync();
                var schoolCount = await dbContext.Schools.CountAsync();
                Console.WriteLine($"Schools in database: {schoolCount}");
            }
            catch (Exception ensureEx)
            {
                Console.WriteLine($"Warning: Could not ensure database: {ensureEx.Message}");
                Console.WriteLine("Please run migrations: dotnet ef database update");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection error: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
        }
    }
}

app.Run();
