using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    [Table("students")]
    public class Student
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("school_id")]
        public int? SchoolId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(20)]
        [Column("gender")]
        public string? Gender { get; set; }

        [MaxLength(20)]
        [Column("aadhar_number")]
        public string? AadharNumber { get; set; }

        [MaxLength(50)]
        [Column("category")]
        public string? Category { get; set; }

        [MaxLength(50)]
        [Column("religion")]
        public string? Religion { get; set; }

        [MaxLength(50)]
        [Column("nationality")]
        public string? Nationality { get; set; }

        [MaxLength(10)]
        [Column("blood_group")]
        public string? BloodGroup { get; set; }

        [MaxLength(50)]
        [Column("mother_tongue")]
        public string? MotherTongue { get; set; }

        [MaxLength(255)]
        [Column("previous_school_name")]
        public string? PreviousSchoolName { get; set; }

        [MaxLength(50)]
        [Column("class_applying_for")]
        public string? ClassApplyingFor { get; set; }

        [MaxLength(50)]
        [Column("medium_of_instruction")]
        public string? MediumOfInstruction { get; set; }

        [MaxLength(255)]
        [Column("father_name")]
        public string? FatherName { get; set; }

        [MaxLength(255)]
        [Column("mother_name")]
        public string? MotherName { get; set; }

        [MaxLength(255)]
        [Column("guardian_name")]
        public string? GuardianName { get; set; }

        [MaxLength(100)]
        [Column("occupation")]
        public string? Occupation { get; set; }

        [MaxLength(100)]
        [Column("educational_qualification")]
        public string? EducationalQualification { get; set; }

        [MaxLength(50)]
        [Column("annual_income")]
        public string? AnnualIncome { get; set; }

        [MaxLength(20)]
        [Column("father_mobile")]
        public string? FatherMobile { get; set; }

        [MaxLength(20)]
        [Column("mother_mobile")]
        public string? MotherMobile { get; set; }

        [MaxLength(255)]
        [Column("parent_email")]
        public string? ParentEmail { get; set; }

        [MaxLength(20)]
        [Column("parent_aadhar")]
        public string? ParentAadhar { get; set; }

        [MaxLength(100)]
        [Column("country")]
        public string? Country { get; set; }

        [Column("permanent_address")]
        public string? PermanentAddress { get; set; }

        [Column("current_address")]
        public string? CurrentAddress { get; set; }

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

        [MaxLength(50)]
        [Column("previous_class_passed")]
        public string? PreviousClassPassed { get; set; }

        [Column("previous_school")]
        public string? PreviousSchool { get; set; }

        [MaxLength(100)]
        [Column("board")]
        public string? Board { get; set; }

        [MaxLength(50)]
        [Column("marks_obtained")]
        public string? MarksObtained { get; set; }

        [MaxLength(50)]
        [Column("tc_number")]
        public string? TcNumber { get; set; }

        [MaxLength(500)]
        [Column("migration_certificate")]
        public string? MigrationCertificate { get; set; }

        [Column("special_needs")]
        public string? SpecialNeeds { get; set; }

        [Column("special_needs_detail")]
        public string? SpecialNeedsDetail { get; set; }

        [MaxLength(255)]
        [Column("emergency_contact")]
        public string? EmergencyContact { get; set; }

        [MaxLength(50)]
        [Column("sibling_in_school")]
        public string? SiblingInSchool { get; set; }

        [MaxLength(50)]
        [Column("transport_required")]
        public string? TransportRequired { get; set; }

        [MaxLength(50)]
        [Column("hostel_required")]
        public string? HostelRequired { get; set; }

        [Column("parent_signature")]
        public string? ParentSignature { get; set; }

        [MaxLength(500)]
        [Column("photo_path")]
        public string? PhotoPath { get; set; }

        [MaxLength(500)]
        [Column("birth_certificate_path")]
        public string? BirthCertificatePath { get; set; }

        [MaxLength(500)]
        [Column("student_aadhar_path")]
        public string? StudentAadharPath { get; set; }

        [MaxLength(500)]
        [Column("parent_aadhar_doc_path")]
        public string? ParentAadharDocPath { get; set; }

        [MaxLength(500)]
        [Column("report_card_path")]
        public string? ReportCardPath { get; set; }

        [MaxLength(500)]
        [Column("transfer_certificate_path")]
        public string? TransferCertificatePath { get; set; }

        [MaxLength(500)]
        [Column("caste_certificate_path")]
        public string? CasteCertificatePath { get; set; }

        [MaxLength(500)]
        [Column("income_certificate_path")]
        public string? IncomeCertificatePath { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("SchoolId")]
        public School? School { get; set; }
    }
}

