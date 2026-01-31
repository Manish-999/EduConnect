using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Entities
{
    [Table("teachers")]
    public class Teacher
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

        [Column("nationality")]
        public string? Nationality { get; set; }

        [Column("blood_group")]
        public string? BloodGroup { get; set; }

        [Column("marital_status")]
        public string? MaritalStatus { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("mobile_number")]
        public string? MobileNumber { get; set; }

        [Column("alternate_mobile")]
        public string? AlternateMobile { get; set; }

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

        [Column("employee_id")]
        public string? EmployeeId { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }

        [Column("department")]
        public string? Department { get; set; }

        [Column("subjects_taught")]
        public string? SubjectsTaught { get; set; }

        [Column("qualification")]
        public string? Qualification { get; set; }

        [Column("specialization")]
        public string? Specialization { get; set; }

        [Column("experience")]
        public string? Experience { get; set; }

        [Column("joining_date")]
        public DateTime? JoiningDate { get; set; }

        [Column("employment_type")]
        public string? EmploymentType { get; set; }

        [Column("highest_qualification")]
        public string? HighestQualification { get; set; }

        [Column("university")]
        public string? University { get; set; }

        [Column("year_of_passing")]
        public int? YearOfPassing { get; set; }

        [Column("percentage")]
        public string? Percentage { get; set; }

        [Column("additional_certifications")]
        public string? AdditionalCertifications { get; set; }

        [Column("previous_school")]
        public string? PreviousSchool { get; set; }

        [Column("previous_designation")]
        public string? PreviousDesignation { get; set; }

        [Column("previous_experience")]
        public string? PreviousExperience { get; set; }

        [Column("reason_for_leaving")]
        public string? ReasonForLeaving { get; set; }

        [Column("basic_salary")]
        public decimal? BasicSalary { get; set; }

        [Column("allowances")]
        public decimal? Allowances { get; set; }

        [Column("bank_name")]
        public string? BankName { get; set; }

        [Column("account_number")]
        public string? AccountNumber { get; set; }

        [Column("ifsc_code")]
        public string? IfscCode { get; set; }

        [Column("pan_number")]
        public string? PanNumber { get; set; }

        [Column("emergency_contact_name")]
        public string? EmergencyContactName { get; set; }

        [Column("emergency_relation")]
        public string? EmergencyRelation { get; set; }

        [Column("emergency_mobile")]
        public string? EmergencyMobile { get; set; }

        [Column("emergency_address")]
        public string? EmergencyAddress { get; set; }

        [Column("terms_accepted")]
        public bool TermsAccepted { get; set; }

        // File properties
        [Column("photo")]
        public byte[]? Photo { get; set; }
        [NotMapped] public IFormFile? PhotoFile { get; set; }

        [Column("resume")]
        public byte[]? Resume { get; set; }
        [NotMapped] public IFormFile? ResumeFile { get; set; }

        [Column("aadhar_card")]
        public byte[]? AadharCard { get; set; }
        [NotMapped] public IFormFile? AadharCardFile { get; set; }

        [Column("certificates")]
        public byte[]? Certificates { get; set; }
        [NotMapped] public IFormFile? CertificatesFile { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("SchoolId")]
        [JsonIgnore] // <-- Add this
        public School? School { get; set; }
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<SectionSubject> SectionSubject { get; set; } = new List<SectionSubject>();
    }
}
