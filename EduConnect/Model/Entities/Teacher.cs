using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    [Table("teachers")]
    public class Teacher
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

        [MaxLength(100)]
        [Column("nationality")]
        public string? Nationality { get; set; }

        [MaxLength(10)]
        [Column("blood_group")]
        public string? BloodGroup { get; set; }

        [MaxLength(20)]
        [Column("marital_status")]
        public string? MaritalStatus { get; set; }

        [MaxLength(100)]
        [Column("country")]
        public string? Country { get; set; }

        [MaxLength(255)]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(20)]
        [Column("mobile_number")]
        public string? MobileNumber { get; set; }

        [MaxLength(20)]
        [Column("alternate_mobile")]
        public string? AlternateMobile { get; set; }

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
        [Column("employee_id")]
        public string? EmployeeId { get; set; }

        [MaxLength(100)]
        [Column("designation")]
        public string? Designation { get; set; }

        [MaxLength(100)]
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

        [MaxLength(50)]
        [Column("employment_type")]
        public string? EmploymentType { get; set; }

        [Column("highest_qualification")]
        public string? HighestQualification { get; set; }

        [MaxLength(255)]
        [Column("university")]
        public string? University { get; set; }

        [Column("year_of_passing")]
        public int? YearOfPassing { get; set; }

        [MaxLength(20)]
        [Column("percentage")]
        public string? Percentage { get; set; }

        [Column("additional_certifications")]
        public string? AdditionalCertifications { get; set; }

        [Column("previous_school")]
        public string? PreviousSchool { get; set; }

        [MaxLength(100)]
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

        [MaxLength(255)]
        [Column("bank_name")]
        public string? BankName { get; set; }

        [MaxLength(50)]
        [Column("account_number")]
        public string? AccountNumber { get; set; }

        [MaxLength(20)]
        [Column("ifsc_code")]
        public string? IfscCode { get; set; }

        [MaxLength(20)]
        [Column("pan_number")]
        public string? PanNumber { get; set; }

        [MaxLength(255)]
        [Column("emergency_contact_name")]
        public string? EmergencyContactName { get; set; }

        [MaxLength(50)]
        [Column("emergency_relation")]
        public string? EmergencyRelation { get; set; }

        [MaxLength(20)]
        [Column("emergency_mobile")]
        public string? EmergencyMobile { get; set; }

        [Column("emergency_address")]
        public string? EmergencyAddress { get; set; }

        [Column("terms_accepted")]
        public bool TermsAccepted { get; set; }

        [MaxLength(500)]
        [Column("photo_path")]
        public string? PhotoPath { get; set; }

        [MaxLength(500)]
        [Column("resume_path")]
        public string? ResumePath { get; set; }

        [MaxLength(500)]
        [Column("aadhar_card_path")]
        public string? AadharCardPath { get; set; }

        [Column("certificates_path")]
        public string? CertificatesPath { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("SchoolId")]
        public School? School { get; set; }
    }
}

