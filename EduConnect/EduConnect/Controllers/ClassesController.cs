using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Model.Entities;
using Services.Interfaces;
namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class ClassesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses(int schoolId) 
        {
            var result = _context.Classes.Where(x => x.SchoolId == schoolId).Select(x=>new ClassResponse()
            {
                Id = x.Id,
                SchoolId = x.SchoolId,
                ClassName = x.ClassName,
                TeacherId = x.ClassTeacher,
                TeacherName = x.Teacher.FirstName + " " + x.Teacher.MiddleName + " " + x.Teacher.LastName,
                Sections = x.Sections.Select(y => new SectionResponse()
                {
                    ClassId = y.ClassId,
                    Id = y.Id,
                    SectionName = y.SectionName,
                }).ToList()
            }).ToList();
            return Ok(result);
        } 

        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassDTO dto)
        {
            var cls = new Class
            {
                SchoolId = dto.SchoolId,
                ClassTeacher = dto.ClassTeacher,
                ClassName = dto.ClassName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            List<Section> section = new();
            foreach (var sectionName in dto.SectionName)
            {
                section.Add(new Section
                {
                    SectionName = sectionName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            cls.Sections = section;
            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();
            return Ok("Class Created Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSection(int classId)
        {
            var result = await _context.Sections.Where(x => x.ClassId == classId)
                .Select(x=>new SectionSubjectResponse()
                {
                    Id=x.Id,
                    ClassId = x.ClassId,
                    ClassName = x.Class.ClassName,
                    SectionName = x.SectionName,
                    Subject = x.SectionSubjects.Select(y=> new SubjectResponse
                    {
                        SectionSubject = y.Id,
                        SubjectId = y.SubjectId,
                        SubjectName = y.Subject.SubjectName,
                        TeacherId = y.TeacherId,
                        TeacherName = y.Teacher.FirstName + " " + y.Teacher.MiddleName + " " + y.Teacher.LastName,
                    }).ToList()
                })
                        .ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSection([FromBody] SectionDTO dto)
        {
            var section = new Section
            {
                ClassId = dto.ClassId,
                SectionName = dto.SectionName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };


            // Assign subjects
            foreach (var subjectId in dto.SubjectIds)
            {
                _context.SectionSubjects.Add(new SectionSubject
                {
                    SectionId = section.Id,
                    SubjectId = subjectId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
            return Ok("section created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSectionSubjects(int sectionId, [FromBody] List<int> subjectIds)
        {
            // 1️⃣ Get the section
            var section = await _context.Sections
                .Include(s => s.SectionSubjects)
                .FirstOrDefaultAsync(s => s.Id == sectionId);

            if (section == null)
                return NotFound($"Section with id {sectionId} not found.");

            // 2️⃣ Remove existing SectionSubjects
            //_context.SectionSubjects.RemoveRange(section.SectionSubjects);

            // 3️⃣ Add new SectionSubjects
            foreach (var subjectId in subjectIds.Distinct()) // remove duplicates
            {
                _context.SectionSubjects.Add(new SectionSubject
                {
                    SectionId = section.Id,
                    SubjectId = subjectId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Section subjects updated successfully",
                sectionId = section.Id,
                subjects = subjectIds
            });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSectionSubjectsteacher(int sectionSubjectId, int teacherId)
        {
            // 1️⃣ Get the section
            var sectionSubject = await _context.SectionSubjects
                .FirstOrDefaultAsync(s => s.Id == sectionSubjectId);

            if (sectionSubject == null)
                return NotFound($"Section subject with id {sectionSubjectId} not found.");

            // 2️⃣ Remove existing SectionSubjects
            //_context.SectionSubjects.RemoveRange(section.SectionSubjects);

            // 3️⃣ Add new SectionSubjects
            sectionSubject.TeacherId = teacherId;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Section subjects updated successfully",
                sectionId = sectionSubject.Id
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubject(int schoolId) => Ok(await _context.Subjects.Where(x=>x.SchoolId==schoolId).ToListAsync());

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDTO dto)
        {
            var subject = new Subject
            {
                SchoolId = dto.SchoolId,
                SubjectName = dto.SubjectName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return Ok(subject);
        }
    }
}

