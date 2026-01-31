using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Model
{
    public class SchoolCreateDto
    {
        [Required]
        public string SchoolName { get; set; } = string.Empty;

        public string? SchoolType { get; set; }
        public string? AffiliationNumber { get; set; }
        public string? SchoolCode { get; set; }
        public string? MediumOfInstruction { get; set; }
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public DateTime? AcademicYearStart { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? Country { get; set; }
        public string? PrincipalName { get; set; }
        public string? Designation { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Aadhar { get; set; }
        public string? PasswordHash { get; set; }
        public bool TermsAccepted { get; set; }

        public IFormFile? SchoolLogoFile { get; set; }
        public IFormFile? AffiliationCertificateFile { get; set; }
    }
}
