using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Entities;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure DateTime to UTC conversion for PostgreSQL
            // PostgreSQL requires UTC DateTime values for timestamp with time zone
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Utc 
                    ? v 
                    : v.Kind == DateTimeKind.Local 
                        ? v.ToUniversalTime() 
                        : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue
                    ? (v.Value.Kind == DateTimeKind.Utc 
                        ? v.Value 
                        : v.Value.Kind == DateTimeKind.Local 
                            ? v.Value.ToUniversalTime() 
                            : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc))
                    : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            // Apply converters to all DateTime properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }

            // Configure School entity
            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.SchoolName).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.SchoolCode).IsUnique();
                entity.HasIndex(e => e.Email);
            });

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.SchoolId);
                entity.HasIndex(e => e.AadharNumber).IsUnique();
                
                // Foreign key relationship
                entity.HasOne(s => s.School)
                    .WithMany()
                    .HasForeignKey(s => s.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Teacher entity
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                // Configure column sizes to match database schema
                entity.Property(e => e.BloodGroup).HasMaxLength(10);
                entity.Property(e => e.Percentage).HasMaxLength(20); // Match database VARCHAR(20)
                
                entity.HasIndex(e => e.SchoolId);
                // Only add unique index if column exists and is unique in database
                // Removed unique constraint on AadharNumber and EmployeeId to avoid conflicts
                // If database has unique constraints, they will be enforced at DB level
                
                // Foreign key relationship
                entity.HasOne(t => t.School)
                    .WithMany()
                    .HasForeignKey(t => t.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

