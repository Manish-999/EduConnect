using DAL;
using DAL.Helpers;
using EduConnect.Helpers;
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
    [Route("api/[action]")]
    //[Authorize]
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveSchool([FromForm] SchoolCreateDto dto)
        {
            try
            {
                var school = new School
                {
                    SchoolName = dto.SchoolName,
                    SchoolType = dto.SchoolType,
                    AffiliationNumber = dto.AffiliationNumber,
                    SchoolCode = dto.SchoolCode,
                    MediumOfInstruction = dto.MediumOfInstruction,
                    TotalStudents = dto.TotalStudents,
                    TotalTeachers = dto.TotalTeachers,
                    AcademicYearStart = dto.AcademicYearStart,
                    AddressLine = dto.AddressLine,
                    City = dto.City,
                    District = dto.District,
                    State = dto.State,
                    PinCode = dto.PinCode,
                    Country = dto.Country,
                    PrincipalName = dto.PrincipalName,
                    Designation = dto.Designation,
                    Mobile = dto.Mobile,
                    Email = dto.Email,
                    Aadhar = dto.Aadhar,
                    PasswordHash = dto.PasswordHash,
                    TermsAccepted = dto.TermsAccepted,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                school.SchoolLogoPath = await FileHelper.SaveFileAsync(dto.SchoolLogoFile, "schools/logos");
                school.AffiliationCertificatePath = await FileHelper.SaveFileAsync(
                    dto.AffiliationCertificateFile,
                    "schools/certificates"
                );

                _context.Schools.Add(school);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, schoolId = school.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to save school", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSchool(int id)
        {
            var school = await _context.Schools
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                return NotFound();

            return Ok(school);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveTeacher([FromForm] TeacherCreateDto dto)
        {
            try
            {
                if (dto.SchoolId <= 0)
                    return BadRequest("SchoolId is required");

                var teacher = new Teacher
                {
                    SchoolId = dto.SchoolId,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    AadharNumber = dto.AadharNumber,
                    Nationality = dto.Nationality,
                    BloodGroup = dto.BloodGroup,
                    MaritalStatus = dto.MaritalStatus,
                    Country = dto.Country,
                    Email = dto.Email,
                    MobileNumber = dto.MobileNumber,
                    AlternateMobile = dto.AlternateMobile,
                    PermanentAddress = dto.PermanentAddress,
                    CurrentAddress = dto.CurrentAddress,
                    City = dto.City,
                    District = dto.District,
                    State = dto.State,
                    PinCode = dto.PinCode,
                    EmployeeId = dto.EmployeeId,
                    Designation = dto.Designation,
                    Department = dto.Department,
                    SubjectsTaught = dto.SubjectsTaught,
                    Qualification = dto.Qualification,
                    Specialization = dto.Specialization,
                    Experience = dto.Experience,
                    JoiningDate = dto.JoiningDate,
                    EmploymentType = dto.EmploymentType,
                    HighestQualification = dto.HighestQualification,
                    University = dto.University,
                    YearOfPassing = dto.YearOfPassing,
                    Percentage = dto.Percentage,
                    AdditionalCertifications = dto.AdditionalCertifications,
                    PreviousSchool = dto.PreviousSchool,
                    PreviousDesignation = dto.PreviousDesignation,
                    PreviousExperience = dto.PreviousExperience,
                    ReasonForLeaving = dto.ReasonForLeaving,
                    BasicSalary = dto.BasicSalary,
                    Allowances = dto.Allowances,
                    BankName = dto.BankName,
                    AccountNumber = dto.AccountNumber,
                    IfscCode = dto.IfscCode,
                    PanNumber = dto.PanNumber,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyRelation = dto.EmergencyRelation,
                    EmergencyMobile = dto.EmergencyMobile,
                    EmergencyAddress = dto.EmergencyAddress,
                    TermsAccepted = dto.TermsAccepted,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Convert files to byte[]
                if (dto.PhotoFile != null)
                {
                    using var ms = new MemoryStream();
                    await dto.PhotoFile.CopyToAsync(ms);
                    teacher.Photo = ms.ToArray();
                }

                if (dto.ResumeFile != null)
                {
                    using var ms = new MemoryStream();
                    await dto.ResumeFile.CopyToAsync(ms);
                    teacher.Resume = ms.ToArray();
                }

                if (dto.AadharCardFile != null)
                {
                    using var ms = new MemoryStream();
                    await dto.AadharCardFile.CopyToAsync(ms);
                    teacher.AadharCard = ms.ToArray();
                }

                if (dto.CertificatesFile != null)
                {
                    using var ms = new MemoryStream();
                    await dto.CertificatesFile.CopyToAsync(ms);
                    teacher.Certificates = ms.ToArray();
                }

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    teacherId = teacher.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error saving teacher",
                    error = ex.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }
        [HttpGet]
        public async Task<IActionResult> GetTeacherPhoto(int id)
        {
            var photo = await _context.Teachers
                .Where(t => t.Id == id)
                .Select(t => t.Photo)
                .FirstOrDefaultAsync();

            if (photo == null)
                return NotFound();

            return File(photo, "image/jpeg");
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveStudent([FromForm] StudentCreateDto dto)
        {
            try
            {
                // Auth is enforced manually so we can return clear JSON errors (not empty 401)
                var auth = await SchoolAccessHelper.ResolveAsync(HttpContext, _context);
                if (auth == null)
                    return Unauthorized(new { message = "Authentication required. Please log in again." });

                var (success, schoolId, errorMessage, statusCode) =
                    await SchoolAccessHelper.ResolveWritableSchoolIdAsync(auth, _context, dto.SchoolId);

                if (!success)
                    return StatusCode(statusCode, new { message = errorMessage });

                var student = new Student
                {
                    SchoolId = schoolId,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    AadharNumber = dto.AadharNumber,
                    Category = dto.Category,
                    Religion = dto.Religion,
                    Nationality = dto.Nationality,
                    BloodGroup = dto.BloodGroup,
                    MotherTongue = dto.MotherTongue,
                    PreviousSchoolName = dto.PreviousSchoolName,
                    ClassApplyingFor = dto.ClassApplyingFor,
                    MediumOfInstruction = dto.MediumOfInstruction,
                    FatherName = dto.FatherName,
                    MotherName = dto.MotherName,
                    GuardianName = dto.GuardianName,
                    Occupation = dto.Occupation,
                    EducationalQualification = dto.EducationalQualification,
                    AnnualIncome = dto.AnnualIncome,
                    FatherMobile = dto.FatherMobile,
                    MotherMobile = dto.MotherMobile,
                    ParentEmail = dto.ParentEmail,
                    ParentAadhar = dto.ParentAadhar,
                    Country = dto.Country,
                    PermanentAddress = dto.PermanentAddress,
                    CurrentAddress = dto.CurrentAddress,
                    City = dto.City,
                    District = dto.District,
                    State = dto.State,
                    PinCode = dto.PinCode,
                    PreviousClassPassed = dto.PreviousClassPassed,
                    PreviousSchool = dto.PreviousSchool,
                    Board = dto.Board,
                    MarksObtained = dto.MarksObtained,
                    TcNumber = dto.TcNumber,
                    MigrationCertificate = dto.MigrationCertificate,
                    SpecialNeeds = dto.SpecialNeeds,
                    SpecialNeedsDetail = dto.SpecialNeedsDetail,
                    EmergencyContact = dto.EmergencyContact,
                    SiblingInSchool = dto.SiblingInSchool,
                    TransportRequired = dto.TransportRequired,
                    HostelRequired = dto.HostelRequired,
                    ParentSignature = dto.ParentSignature,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                student.PhotoPath = await FileHelper.SaveFileAsync(dto.PhotoFile, "students/photos");
                student.BirthCertificatePath = await FileHelper.SaveFileAsync(dto.BirthCertificateFile, "students/documents");
                student.StudentAadharPath = await FileHelper.SaveFileAsync(dto.StudentAadharFile, "students/documents");
                student.ParentAadharDocPath = await FileHelper.SaveFileAsync(dto.ParentAadharDocFile, "students/documents");
                student.ReportCardPath = await FileHelper.SaveFileAsync(dto.ReportCardFile, "students/documents");
                student.TransferCertificatePath = await FileHelper.SaveFileAsync(dto.TransferCertificateFile, "students/documents");
                student.CasteCertificatePath = await FileHelper.SaveFileAsync(dto.CasteCertificateFile, "students/documents");
                student.IncomeCertificatePath = await FileHelper.SaveFileAsync(dto.IncomeCertificateFile, "students/documents");

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, studentId = student.Id, schoolId = student.SchoolId });
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new
                {
                    message = "Error saving student",
                    error = detail
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
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
                return StatusCode(500, new { message = "Error retrieving schools", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers([FromQuery] int schoolId)
        {
            if (schoolId <= 0)
                return BadRequest(new { message = "SchoolId is required" });

            try
            {
                var teachers = await _context.Teachers
                    .Where(t => t.SchoolId == schoolId)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();

                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving teachers", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] int schoolId, [FromQuery] int sectionId = 0)
        {
            try
            {
                var auth = await SchoolAccessHelper.ResolveAsync(HttpContext, _context);
                if (auth == null)
                    return Unauthorized(new { message = "Authentication required. Please log in again." });

                var (success, effectiveSchoolId, errorMessage, statusCode) =
                    await SchoolAccessHelper.ResolveReadableSchoolIdAsync(auth, _context, schoolId);

                if (!success)
                    return StatusCode(statusCode, new { message = errorMessage });

                var query = _context.Students
                    .AsNoTracking()
                    .Where(s => s.SchoolId == effectiveSchoolId);

                // section_id is not yet in the students table schema
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
