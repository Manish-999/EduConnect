using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Model
{
    public class StudentRegistrationRequest
    {
        // Student Basic Details
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string AadharNumber { get; set; }

        public string Category { get; set; }

        public string Religion { get; set; }

        public string Nationality { get; set; }

        public string? BloodGroup { get; set; }

        public string? MotherTongue { get; set; }

        public string? PreviousSchoolName { get; set; }

        public string ClassApplyingFor { get; set; }

        public string? MediumOfInstruction { get; set; }

        // Parent / Guardian Details
        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string GuardianName { get; set; }

        public string Occupation { get; set; }

        public string EducationalQualification { get; set; }

        public string AnnualIncome { get; set; }

        public string FatherMobile { get; set; }

        public string? MotherMobile { get; set; }

        [EmailAddress]
        public string? ParentEmail { get; set; }

        public string? ParentAadhar { get; set; }

        // Address Details
        public string Country { get; set; }

        public string PermanentAddress { get; set; }

        public string CurrentAddress { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string State { get; set; }

        public string PinCode { get; set; }

        // Academic Details
        public string PreviousClassPassed { get; set; }

        public string PreviousSchool { get; set; }

        public string? Board { get; set; }

        public string? MarksObtained { get; set; }

        public string? TcNumber { get; set; }

        public string? MigrationCertificate { get; set; }

        // Other Details
        public string? SpecialNeeds { get; set; }

        public string? SpecialNeedsDetail { get; set; }

        public string EmergencyContact { get; set; }

        public string? SiblingInSchool { get; set; }

        public string? TransportRequired { get; set; }

        public string? HostelRequired { get; set; }

        public string ParentSignature { get; set; }

        // Documents (File Uploads)
        //public IFormFile? Photo { get; set; }

        //public IFormFile? BirthCertificate { get; set; }

        //public IFormFile? StudentAadhar { get; set; }

        //public IFormFile? ParentAadharDoc { get; set; }

        //public IFormFile? ReportCard { get; set; }

        //public IFormFile? TransferCertificate { get; set; }

        //public IFormFile? CasteCertificate { get; set; }

        //public IFormFile? IncomeCertificate { get; set; }
    }
}
