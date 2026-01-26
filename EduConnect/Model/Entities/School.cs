using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    [Table("schools")]
    public class School
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("school_name")]
        public string SchoolName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("school_type")]
        public string? SchoolType { get; set; }

        [MaxLength(100)]
        [Column("affiliation_number")]
        public string? AffiliationNumber { get; set; }

        [MaxLength(50)]
        [Column("school_code")]
        public string? SchoolCode { get; set; }

        [MaxLength(50)]
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

        [MaxLength(100)]
        [Column("city")]
        public string? City { get; set; }

        [MaxLength(100)]
        [Column("district")]
        public string? District { get; set; }

        [MaxLength(100)]
        [Column("state")]
        public string? State { get; set; }

        [MaxLength(20)]
        [Column("pin_code")]
        public string? PinCode { get; set; }

        [MaxLength(100)]
        [Column("country")]
        public string? Country { get; set; }

        [MaxLength(255)]
        [Column("principal_name")]
        public string? PrincipalName { get; set; }

        [MaxLength(100)]
        [Column("designation")]
        public string? Designation { get; set; }

        [MaxLength(20)]
        [Column("mobile")]
        public string? Mobile { get; set; }

        [MaxLength(255)]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(20)]
        [Column("aadhar")]
        public string? Aadhar { get; set; }

        [MaxLength(255)]
        [Column("password_hash")]
        public string? PasswordHash { get; set; }

        [Column("terms_accepted")]
        public bool TermsAccepted { get; set; }

        [MaxLength(500)]
        [Column("school_logo_path")]
        public string? SchoolLogoPath { get; set; }

        [MaxLength(500)]
        [Column("affiliation_certificate_path")]
        public string? AffiliationCertificatePath { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

