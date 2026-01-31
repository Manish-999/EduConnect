using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entities
{
    [Table("schools")]
    public class School
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("school_name")]
        public string SchoolName { get; set; } = string.Empty;

        [Column("school_type")]
        public string? SchoolType { get; set; }

        [Column("affiliation_number")]
        public string? AffiliationNumber { get; set; }

        [Column("school_code")]
        public string? SchoolCode { get; set; }

        [Column("medium_of_instruction")]
        public string? MediumOfInstruction { get; set; }

        [Column("total_students")]
        public int TotalStudents { get; set; }

        [Column("total_teachers")]
        public int TotalTeachers { get; set; }

        [Column("academic_year_start")]
        public DateTime? AcademicYearStart { get; set; }

        [Column("address_line")]
        public string? AddressLine { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("district")]
        public string? District { get; set; }

        [Column("state")]
        public string? State { get; set; }

        [Column("pin_code")]
        public string? PinCode { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("principal_name")]
        public string? PrincipalName { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }

        [Column("mobile")]
        public string? Mobile { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("aadhar")]
        public string? Aadhar { get; set; }

        [Column("password_hash")]
        public string? PasswordHash { get; set; }

        [Column("terms_accepted")]
        public bool TermsAccepted { get; set; }

        [Column("school_logo")]
        public byte[]? SchoolLogo { get; set; }

        [NotMapped]
        public IFormFile? SchoolLogoFile { get; set; }

        [Column("affiliation_certificate")]
        public byte[]? AffiliationCertificate { get; set; }

        [NotMapped]
        public IFormFile? AffiliationCertificateFile { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore] // <-- Add this
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        [JsonIgnore] // <-- Add this
        public ICollection<Student> Students { get; set; } = new List<Student>(); 
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
