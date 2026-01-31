using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class StudentCreateDto
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
        public string? Category { get; set; }
        public string? Religion { get; set; }
        public string? Nationality { get; set; }
        public string? BloodGroup { get; set; }
        public string? MotherTongue { get; set; }
        public string? PreviousSchoolName { get; set; }
        public string? ClassApplyingFor { get; set; }
        public string? MediumOfInstruction { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? GuardianName { get; set; }
        public string? Occupation { get; set; }
        public string? EducationalQualification { get; set; }
        public string? AnnualIncome { get; set; }
        public string? FatherMobile { get; set; }
        public string? MotherMobile { get; set; }
        public string? ParentEmail { get; set; }
        public string? ParentAadhar { get; set; }
        public string? Country { get; set; }
        public string? PermanentAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? PreviousClassPassed { get; set; }
        public string? PreviousSchool { get; set; }
        public string? Board { get; set; }
        public string? MarksObtained { get; set; }
        public string? TcNumber { get; set; }
        public string? MigrationCertificate { get; set; }
        public string? SpecialNeeds { get; set; }
        public string? SpecialNeedsDetail { get; set; }
        public string? EmergencyContact { get; set; }
        public string? SiblingInSchool { get; set; }
        public string? TransportRequired { get; set; }
        public string? HostelRequired { get; set; }
        public string? ParentSignature { get; set; }
        public int SectionId { get; set; }

        // Files
        public IFormFile? PhotoFile { get; set; }
        public IFormFile? BirthCertificateFile { get; set; }
        public IFormFile? StudentAadharFile { get; set; }
        public IFormFile? ParentAadharDocFile { get; set; }
        public IFormFile? ReportCardFile { get; set; }
        public IFormFile? TransferCertificateFile { get; set; }
        public IFormFile? CasteCertificateFile { get; set; }
        public IFormFile? IncomeCertificateFile { get; set; }
    }
}
