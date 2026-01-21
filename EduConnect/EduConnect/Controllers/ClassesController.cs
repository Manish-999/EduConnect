using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs;
using Services.Interfaces;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        private readonly IInMemorySchoolStore _store;

        public ClassesController(IInMemorySchoolStore store)
        {
            _store = store;
        }

        [HttpPost]
        public IActionResult CreateClass([FromBody] CreateClassRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ClassName))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "ClassName is required",
                    Data = null
                });
            }

            var classDto = new ClassDto
            {
                ClassName = request.ClassName,
                Section = request.Section,
                ClassTeacherId = request.ClassTeacherId
            };

            // Validate ClassTeacherId if provided
            if (request.ClassTeacherId.HasValue)
            {
                var teacher = _store.GetTeacherById(request.ClassTeacherId.Value);
                if (teacher == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Teacher with Id {request.ClassTeacherId.Value} not found",
                        Data = null
                    });
                }
            }

            var createdClass = _store.AddClass(classDto);

            return Ok(new ApiResponse<ClassDto>
            {
                Success = true,
                Message = "Class created successfully",
                Data = createdClass
            });
        }

        [HttpGet]
        public IActionResult GetAllClasses()
        {
            var classes = _store.GetAllClasses();

            return Ok(new ApiResponse<List<ClassDto>>
            {
                Success = true,
                Message = "Classes retrieved successfully",
                Data = classes
            });
        }

        [HttpGet("{classId:int}/subjects")]
        public IActionResult GetClassSubjects([FromRoute(Name = "classId")] int classId)
        {
            if (classId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "classId is required and must be greater than 0",
                    Data = null
                });
            }

            var classEntity = _store.GetClassById(classId);
            if (classEntity == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Class with Id {classId} not found",
                    Data = null
                });
            }

            var subjects = _store.GetClassSubjects(classId);

            return Ok(new ApiResponse<List<ClassSubjectResponseDto>>
            {
                Success = true,
                Message = "Subjects retrieved successfully",
                Data = subjects
            });
        }

        [HttpPost("{classId:int}/subjects")]
        public IActionResult AssignSubjectsToClass([FromRoute(Name = "classId")] int classId, [FromBody] AssignSubjectsRequest request)
        {
            if (classId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "classId is required and must be greater than 0",
                    Data = null
                });
            }

            if (request == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Request body is required",
                    Data = null
                });
            }

            var classEntity = _store.GetClassById(classId);
            if (classEntity == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Class with Id {classId} not found",
                    Data = null
                });
            }

            if (request.SubjectIds == null || request.SubjectIds.Count == 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "At least one subjectId is required",
                    Data = null
                });
            }

            // Validate all subject IDs exist
            var invalidSubjectIds = new List<int>();
            foreach (var subjectId in request.SubjectIds)
            {
                var subject = _store.GetSubjectById(subjectId);
                if (subject == null)
                {
                    invalidSubjectIds.Add(subjectId);
                }
            }

            if (invalidSubjectIds.Count > 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Invalid subject IDs: {string.Join(", ", invalidSubjectIds)}",
                    Data = null
                });
            }

            // Check for duplicates
            var duplicateSubjectIds = request.SubjectIds
                .GroupBy(id => id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateSubjectIds.Count > 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Duplicate subject IDs found: {string.Join(", ", duplicateSubjectIds)}",
                    Data = null
                });
            }

            _store.AssignSubjectsToClass(classId, request.SubjectIds);

            var updatedSubjects = _store.GetClassSubjects(classId);

            return Ok(new ApiResponse<List<ClassSubjectResponseDto>>
            {
                Success = true,
                Message = "Subjects assigned to class successfully",
                Data = updatedSubjects
            });
        }

        [HttpPut("{classId:int}/subjects/{subjectId:int}/teacher")]
        public IActionResult AssignTeacherToClassSubject([FromRoute(Name = "classId")] int classId, [FromRoute(Name = "subjectId")] int subjectId, [FromBody] AssignTeacherRequest request)
        {
            if (classId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "classId is required and must be greater than 0",
                    Data = null
                });
            }

            if (subjectId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "subjectId is required and must be greater than 0",
                    Data = null
                });
            }

            if (request == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Request body is required",
                    Data = null
                });
            }

            var classEntity = _store.GetClassById(classId);
            if (classEntity == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Class with Id {classId} not found",
                    Data = null
                });
            }

            var subject = _store.GetSubjectById(subjectId);
            if (subject == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Subject with Id {subjectId} not found",
                    Data = null
                });
            }

            var teacher = _store.GetTeacherById(request.TeacherId);
            if (teacher == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Teacher with Id {request.TeacherId} not found",
                    Data = null
                });
            }

            // Assign teacher to class subject (store method will create mapping if it doesn't exist)
            _store.AssignTeacherToClassSubject(classId, subjectId, request.TeacherId);

            var updatedSubjects = _store.GetClassSubjects(classId);
            var updatedSubject = updatedSubjects.FirstOrDefault(s => s.SubjectId == subjectId);

            return Ok(new ApiResponse<ClassSubjectResponseDto>
            {
                Success = true,
                Message = "Teacher assigned to class subject successfully",
                Data = updatedSubject
            });
        }
    }
}

