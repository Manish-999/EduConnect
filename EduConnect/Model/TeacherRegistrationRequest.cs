using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Model
{
    public class TeacherRegistrationRequest
    {
        // Personal Details
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string AadharNumber { get; set; }

        public string? Nationality { get; set; }

        public string? BloodGroup { get; set; }

        public string? MaritalStatus { get; set; }

        // Contact Details
        public string Country { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string? AlternateMobile { get; set; }

        // Address Details
        public string PermanentAddress { get; set; }

        public string? CurrentAddress { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string State { get; set; }

        public string PinCode { get; set; }

        // Employment Details
        public string EmployeeId { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Subjects { get; set; }

        public string Qualification { get; set; }

        public string? Specialization { get; set; }

        public string? Experience { get; set; }

        public DateTime JoiningDate { get; set; }

        public string? EmploymentType { get; set; }

        // Education Details
        public string HighestQualification { get; set; }

        public string University { get; set; }

        public int YearOfPassing { get; set; }

        public string? Percentage { get; set; }

        public string? AdditionalCertifications { get; set; }

        // Previous Employment
        public string? PreviousSchool { get; set; }

        public string? PreviousDesignation { get; set; }

        public string? PreviousExperience { get; set; }

        public string? ReasonForLeaving { get; set; }

        // Salary & Bank Details
        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string IfscCode { get; set; }

        public string PanNumber { get; set; }

        // Emergency Contact
        public string EmergencyContactName { get; set; }

        public string? EmergencyRelation { get; set; }

        public string EmergencyMobile { get; set; }

        public string? EmergencyAddress { get; set; }

        // Agreement
        public bool TermsAccepted { get; set; }

        // Documents (File Uploads)
        public IFormFile? Photo { get; set; }

        public IFormFile? Resume { get; set; }

        public IFormFile? AadharCard { get; set; }

        public IFormFile? Certificates { get; set; }
    }
}
