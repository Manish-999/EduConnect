using DAL;
using DAL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Model.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("tc/[action]")]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISchoolService _schoolService;

        public SchoolController(ApplicationDbContext context, ISchoolService schoolService)
        {
            _context = context;
            _schoolService = schoolService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSchool([FromForm] SchoolRegistrationRequest request)
        {
            try
            {
                // Handle file uploads
                var schoolLogoPath = await FileHelper.SaveFileAsync(request.SchoolLogo, "schools/logos");
                var affiliationCertPath = await FileHelper.SaveFileAsync(request.AffiliationCertificate, "schools/certificates");

                // Create School entity
                var school = new School
                {
                    SchoolName = request.SchoolName,
                    SchoolType = request.SchoolType,
                    AffiliationNumber = request.AffiliationNumber,
                    SchoolCode = request.SchoolCode,
                    MediumOfInstruction = request.MediumOfInstruction,
                    TotalStudents = request.TotalStudents,
                    TotalTeachers = request.TotalTeachers,
                    AcademicYearStart = request.AcademicYearStart != default ? DateTime.SpecifyKind(request.AcademicYearStart, DateTimeKind.Utc) : null,
                    AddressLine = request.AddressLine,
                    City = request.City,
                    District = request.District,
                    State = request.State,
                    PinCode = request.PinCode,
                    Country = request.Country ?? "India",
                    PrincipalName = request.PrincipalName,
                    Designation = request.Designation,
                    Mobile = request.Mobile,
                    Email = request.Email,
                    Aadhar = request.Aadhar,
                    PasswordHash = request.Password, // TODO: Hash password properly
                    TermsAccepted = request.TermsAccepted,
                    SchoolLogoPath = schoolLogoPath,
                    AffiliationCertificatePath = affiliationCertPath,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Schools.Add(school);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, schoolId = school.Id });
            }
            catch (Exception ex)
            {
                // Show inner exception details for debugging
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner: {ex.InnerException.Message}";
                }
                return StatusCode(500, new { message = "Error saving school", error = errorMessage, fullError = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveTeacher([FromForm] TeacherRegistrationRequest request)
        {
            try
            {
                // Auto-populate SchoolId if not provided
                if (request.SchoolId == null || request.SchoolId == 0)
                {
                    if (Request.Form.TryGetValue("SchoolId", out var schoolIdValue) && 
                        int.TryParse(schoolIdValue.ToString(), out int schoolId) && schoolId > 0)
                    {
                        request.SchoolId = schoolId;
                    }
                }

                // Validate SchoolId is set before proceeding
                if (!request.SchoolId.HasValue || request.SchoolId.Value <= 0)
                {
                    return BadRequest(new { success = false, message = "SchoolId is required to register a teacher. Please ensure a school is selected." });
                }

                // Debug: Log the SchoolId being saved
                Console.WriteLine($"SaveTeacher - Saving teacher with SchoolId: {request.SchoolId.Value}");

                // Handle file uploads
                var photoPath = await FileHelper.SaveFileAsync(request.Photo, "teachers/photos");
                var resumePath = await FileHelper.SaveFileAsync(request.Resume, "teachers/resumes");
                var aadharCardPath = await FileHelper.SaveFileAsync(request.AadharCard, "teachers/documents");
                var certificatesPath = await FileHelper.SaveFileAsync(request.Certificates, "teachers/certificates");

                // Create Teacher entity
                // Note: DateTime conversion to UTC is handled automatically by ApplicationDbContext converter
                var teacher = new Teacher
                {
                    SchoolId = request.SchoolId,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth != default ? request.DateOfBirth : null,
                    Gender = request.Gender,
                    AadharNumber = request.AadharNumber,
                    Nationality = request.Nationality,
                    BloodGroup = request.BloodGroup,
                    MaritalStatus = request.MaritalStatus,
                    Country = request.Country,
                    Email = request.Email,
                    MobileNumber = request.MobileNumber,
                    AlternateMobile = request.AlternateMobile,
                    PermanentAddress = request.PermanentAddress,
                    CurrentAddress = request.CurrentAddress,
                    City = request.City,
                    District = request.District,
                    State = request.State,
                    PinCode = request.PinCode,
                    EmployeeId = request.EmployeeId,
                    Designation = request.Designation,
                    Department = request.Department,
                    SubjectsTaught = request.Subjects,
                    Qualification = request.Qualification,
                    Specialization = request.Specialization,
                    Experience = request.Experience,
                    JoiningDate = request.JoiningDate != default ? request.JoiningDate : null,
                    EmploymentType = request.EmploymentType,
                    HighestQualification = request.HighestQualification,
                    University = request.University,
                    YearOfPassing = request.YearOfPassing != default ? request.YearOfPassing : null,
                    Percentage = request.Percentage,
                    AdditionalCertifications = request.AdditionalCertifications,
                    PreviousSchool = request.PreviousSchool,
                    PreviousDesignation = request.PreviousDesignation,
                    PreviousExperience = request.PreviousExperience,
                    ReasonForLeaving = request.ReasonForLeaving,
                    BasicSalary = request.BasicSalary != default ? request.BasicSalary : null,
                    Allowances = request.Allowances != default ? request.Allowances : null,
                    BankName = request.BankName,
                    AccountNumber = request.AccountNumber,
                    IfscCode = request.IfscCode,
                    PanNumber = request.PanNumber,
                    EmergencyContactName = request.EmergencyContactName,
                    EmergencyRelation = request.EmergencyRelation,
                    EmergencyMobile = request.EmergencyMobile,
                    EmergencyAddress = request.EmergencyAddress,
                    TermsAccepted = request.TermsAccepted,
                    PhotoPath = photoPath,
                    ResumePath = resumePath,
                    AadharCardPath = aadharCardPath,
                    CertificatesPath = certificatesPath,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();

                // Verify the teacher was saved with correct SchoolId
                Console.WriteLine($"SaveTeacher - Teacher saved successfully. TeacherId: {teacher.Id}, SchoolId: {teacher.SchoolId}");

                return Ok(new { success = true, teacherId = teacher.Id, schoolId = teacher.SchoolId });
            }
            catch (Exception ex)
            {
                // Show inner exception details for debugging
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner: {ex.InnerException.Message}";
                }
                return StatusCode(500, new { message = "Error saving teacher", error = errorMessage, fullError = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveStudent([FromForm] StudentRegistrationRequest request)
        {
            try
            {
                // Auto-populate SchoolId if not provided
                if (request.SchoolId == null || request.SchoolId == 0)
                {
                    if (Request.Form.TryGetValue("SchoolId", out var schoolIdValue) && 
                        int.TryParse(schoolIdValue.ToString(), out int schoolId) && schoolId > 0)
                    {
                        request.SchoolId = schoolId;
                    }
                }

                // Handle file uploads
                var photoPath = await FileHelper.SaveFileAsync(request.Photo, "students/photos");
                var birthCertPath = await FileHelper.SaveFileAsync(request.BirthCertificate, "students/documents");
                var studentAadharPath = await FileHelper.SaveFileAsync(request.StudentAadhar, "students/documents");
                var parentAadharDocPath = await FileHelper.SaveFileAsync(request.ParentAadharDoc, "students/documents");
                var reportCardPath = await FileHelper.SaveFileAsync(request.ReportCard, "students/documents");
                var transferCertPath = await FileHelper.SaveFileAsync(request.TransferCertificate, "students/documents");
                var casteCertPath = await FileHelper.SaveFileAsync(request.CasteCertificate, "students/documents");
                var incomeCertPath = await FileHelper.SaveFileAsync(request.IncomeCertificate, "students/documents");

                // Create Student entity
                // Note: DateTime conversion to UTC is handled automatically by ApplicationDbContext converter
                var student = new Student
                {
                    SchoolId = request.SchoolId,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth != default ? request.DateOfBirth : null,
                    Gender = request.Gender,
                    AadharNumber = request.AadharNumber,
                    Category = request.Category,
                    Religion = request.Religion,
                    Nationality = request.Nationality,
                    BloodGroup = request.BloodGroup,
                    MotherTongue = request.MotherTongue,
                    PreviousSchoolName = request.PreviousSchoolName,
                    ClassApplyingFor = request.ClassApplyingFor,
                    MediumOfInstruction = request.MediumOfInstruction,
                    FatherName = request.FatherName,
                    MotherName = request.MotherName,
                    GuardianName = request.GuardianName,
                    Occupation = request.Occupation,
                    EducationalQualification = request.EducationalQualification,
                    AnnualIncome = request.AnnualIncome,
                    FatherMobile = request.FatherMobile,
                    MotherMobile = request.MotherMobile,
                    ParentEmail = request.ParentEmail,
                    ParentAadhar = request.ParentAadhar,
                    Country = request.Country,
                    PermanentAddress = request.PermanentAddress,
                    CurrentAddress = request.CurrentAddress,
                    City = request.City,
                    District = request.District,
                    State = request.State,
                    PinCode = request.PinCode,
                    PreviousClassPassed = request.PreviousClassPassed,
                    PreviousSchool = request.PreviousSchool,
                    Board = request.Board,
                    MarksObtained = request.MarksObtained,
                    TcNumber = request.TcNumber,
                    MigrationCertificate = request.MigrationCertificate,
                    SpecialNeeds = request.SpecialNeeds,
                    SpecialNeedsDetail = request.SpecialNeedsDetail,
                    EmergencyContact = request.EmergencyContact,
                    SiblingInSchool = request.SiblingInSchool,
                    TransportRequired = request.TransportRequired,
                    HostelRequired = request.HostelRequired,
                    ParentSignature = request.ParentSignature,
                    PhotoPath = photoPath,
                    BirthCertificatePath = birthCertPath,
                    StudentAadharPath = studentAadharPath,
                    ParentAadharDocPath = parentAadharDocPath,
                    ReportCardPath = reportCardPath,
                    TransferCertificatePath = transferCertPath,
                    CasteCertificatePath = casteCertPath,
                    IncomeCertificatePath = incomeCertPath,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, studentId = student.Id });
            }
            catch (Exception ex)
            {
                // Show inner exception details for debugging
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner: {ex.InnerException.Message}";
                }
                return StatusCode(500, new { message = "Error saving student", error = errorMessage, fullError = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetAllSchool()
        {
            try
            {
                var schools = await _context.Schools
                    .OrderByDescending(s => s.CreatedAt)
                    .ToListAsync();

                return Ok(schools);
            }
            catch (Exception ex)
            {
                // Show inner exception details for debugging
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner: {ex.InnerException.Message}";
                }
                return StatusCode(500, new { message = "Error retrieving schools", error = errorMessage, fullError = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetAllTeacher([FromBody] GetAllTeacherRequest? request = null)
        {
            try
            {
                var query = _context.Teachers.AsQueryable();
                
                // Filter by SchoolId if provided
                int? schoolId = request?.SchoolId;
                if (schoolId.HasValue && schoolId.Value > 0)
                {
                    query = query.Where(t => t.SchoolId == schoolId.Value);
                }

                var teachers = await query
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();

                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving teachers", error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetAllStudent([FromBody] GetAllStudentRequest? request = null)
        {
            try
            {
                var query = _context.Students.AsQueryable();
                
                // Filter by SchoolId if provided
                int? schoolId = request?.SchoolId;
                if (schoolId.HasValue && schoolId.Value > 0)
                {
                    query = query.Where(s => s.SchoolId == schoolId.Value);
                }

                var students = await query
                    .OrderByDescending(s => s.CreatedAt)
                    .ToListAsync();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving students", error = ex.Message });
            }
        }
    }
}
