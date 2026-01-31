using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class TeacherCreateDto
    {
        [Required]
        public int SchoolId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? AadharNumber { get; set; }
        public string? Nationality { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? AlternateMobile { get; set; }
        public string? PermanentAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? EmployeeId { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public string? SubjectsTaught { get; set; }
        public string? Qualification { get; set; }
        public string? Specialization { get; set; }
        public string? Experience { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? EmploymentType { get; set; }
        public string? HighestQualification { get; set; }
        public string? University { get; set; }
        public int? YearOfPassing { get; set; }
        public string? Percentage { get; set; }
        public string? AdditionalCertifications { get; set; }
        public string? PreviousSchool { get; set; }
        public string? PreviousDesignation { get; set; }
        public string? PreviousExperience { get; set; }
        public string? ReasonForLeaving { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Allowances { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IfscCode { get; set; }
        public string? PanNumber { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyRelation { get; set; }
        public string? EmergencyMobile { get; set; }
        public string? EmergencyAddress { get; set; }
        public bool TermsAccepted { get; set; }

        // Files
        public IFormFile? PhotoFile { get; set; }
        public IFormFile? ResumeFile { get; set; }
        public IFormFile? AadharCardFile { get; set; }
        public IFormFile? CertificatesFile { get; set; }
    }
}
