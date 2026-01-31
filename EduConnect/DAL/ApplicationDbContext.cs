using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SectionSubject> SectionSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------- School ----------------
            modelBuilder.Entity<School>()
                .ToTable("schools")
                .HasKey(s => s.Id);

            // Navigation: School -> Classes
            modelBuilder.Entity<School>()
                .HasMany(s => s.Classes)
                .WithOne(c => c.School)
                .HasForeignKey(c => c.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            // Navigation: School -> Teachers
            modelBuilder.Entity<School>()
                .HasMany(s => s.Teachers)
                .WithOne(t => t.School)
                .HasForeignKey(t => t.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Navigation: School -> Students
            modelBuilder.Entity<School>()
                .HasMany(s => s.Students)
                .WithOne(st => st.School)
                .HasForeignKey(st => st.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------- Class ----------------
            modelBuilder.Entity<Class>()
                .ToTable("classes")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Sections)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- Section ----------------
            modelBuilder.Entity<Section>()
                .ToTable("sections")
                .HasKey(s => s.Id);

            modelBuilder.Entity<Section>()
                .HasMany(s => s.Students)
                .WithOne(st => st.Section)
                .HasForeignKey(st => st.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Section>()
                .HasMany(s => s.SectionSubjects)
                .WithOne(ss => ss.Section)
                .HasForeignKey(ss => ss.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- Subject ----------------
            modelBuilder.Entity<Subject>()
                .ToTable("subjects")
                .HasKey(sub => sub.Id);

            modelBuilder.Entity<Subject>()
                .HasMany(sub => sub.SectionSubjects)
                .WithOne(ss => ss.Subject)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- SectionSubjects ----------------
            modelBuilder.Entity<SectionSubject>()
                .ToTable("section_subjects")
                .HasKey(ss => ss.Id);

            // ---------------- Student ----------------
            modelBuilder.Entity<Student>()
                .ToTable("students")
                .HasKey(s => s.Id);

            // ---------------- Teacher ----------------
            modelBuilder.Entity<Teacher>()
                .ToTable("teachers")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Teacher>()
                .HasMany(sub => sub.Classes)
                .WithOne(ss => ss.Teacher)
                .HasForeignKey(ss => ss.ClassTeacher)
                .OnDelete(DeleteBehavior.Cascade); 
            
            modelBuilder.Entity<Teacher>()
                .HasMany(sub => sub.SectionSubject)
                .WithOne(ss => ss.Teacher)
                .HasForeignKey(ss => ss.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- User ----------------
            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasKey(u => u.Id);
        }
    }
}
