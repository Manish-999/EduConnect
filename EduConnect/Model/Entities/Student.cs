using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entities
{
    [Table("students")]
    public class Student
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("school_id")]
        public int SchoolId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("aadhar_number")]
        public string? AadharNumber { get; set; }

        [Column("category")]
        public string? Category { get; set; }

        [Column("religion")]
        public string? Religion { get; set; }

        [Column("nationality")]
        public string? Nationality { get; set; }

        [Column("blood_group")]
        public string? BloodGroup { get; set; }

        [Column("mother_tongue")]
        public string? MotherTongue { get; set; }

        [Column("previous_school_name")]
        public string? PreviousSchoolName { get; set; }

        [Column("class_applying_for")]
        public string? ClassApplyingFor { get; set; }

        [Column("medium_of_instruction")]
        public string? MediumOfInstruction { get; set; }

        [Column("father_name")]
        public string? FatherName { get; set; }

        [Column("mother_name")]
        public string? MotherName { get; set; }

        [Column("guardian_name")]
        public string? GuardianName { get; set; }

        [Column("occupation")]
        public string? Occupation { get; set; }

        [Column("educational_qualification")]
        public string? EducationalQualification { get; set; }

        [Column("annual_income")]
        public string? AnnualIncome { get; set; }

        [Column("father_mobile")]
        public string? FatherMobile { get; set; }

        [Column("mother_mobile")]
        public string? MotherMobile { get; set; }

        [Column("parent_email")]
        public string? ParentEmail { get; set; }

        [Column("parent_aadhar")]
        public string? ParentAadhar { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("permanent_address")]
        public string? PermanentAddress { get; set; }

        [Column("current_address")]
        public string? CurrentAddress { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("district")]
        public string? District { get; set; }

        [Column("state")]
        public string? State { get; set; }

        [Column("pin_code")]
        public string? PinCode { get; set; }

        [Column("previous_class_passed")]
        public string? PreviousClassPassed { get; set; }

        [Column("previous_school")]
        public string? PreviousSchool { get; set; }

        [Column("board")]
        public string? Board { get; set; }

        [Column("marks_obtained")]
        public string? MarksObtained { get; set; }

        [Column("tc_number")]
        public string? TcNumber { get; set; }

        [Column("migration_certificate")]
        public string? MigrationCertificate { get; set; }

        [Column("special_needs")]
        public string? SpecialNeeds { get; set; }

        [Column("special_needs_detail")]
        public string? SpecialNeedsDetail { get; set; }

        [Column("emergency_contact")]
        public string? EmergencyContact { get; set; }

        [Column("sibling_in_school")]
        public string? SiblingInSchool { get; set; }

        [Column("transport_required")]
        public string? TransportRequired { get; set; }

        [Column("hostel_required")]
        public string? HostelRequired { get; set; }

        [Column("parent_signature")]
        public string? ParentSignature { get; set; }

        // File properties
        [Column("photo")]
        public byte[]? Photo { get; set; }
        [NotMapped] public IFormFile? PhotoFile { get; set; }

        [Column("birth_certificate")]
        public byte[]? BirthCertificate { get; set; }
        [NotMapped] public IFormFile? BirthCertificateFile { get; set; }

        [Column("student_aadhar")]
        public byte[]? StudentAadhar { get; set; }
        [NotMapped] public IFormFile? StudentAadharFile { get; set; }

        [Column("parent_aadhar_doc")]
        public byte[]? ParentAadharDoc { get; set; }
        [NotMapped] public IFormFile? ParentAadharDocFile { get; set; }

        [Column("report_card")]
        public byte[]? ReportCard { get; set; }
        [NotMapped] public IFormFile? ReportCardFile { get; set; }

        [Column("transfer_certificate")]
        public byte[]? TransferCertificate { get; set; }
        [NotMapped] public IFormFile? TransferCertificateFile { get; set; }

        [Column("caste_certificate")]
        public byte[]? CasteCertificate { get; set; }
        [NotMapped] public IFormFile? CasteCertificateFile { get; set; }

        [Column("income_certificate")]
        public byte[]? IncomeCertificate { get; set; }
        [NotMapped] public IFormFile? IncomeCertificateFile { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Column("section_id")]
        public int SectionId { get; set; }

        [ForeignKey("SchoolId")]
        [JsonIgnore] // <-- Add this
        public School? School { get; set; }
        [ForeignKey("SectionId")]
        public Section? Section { get; set; }
    }
}
