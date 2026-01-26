using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Model.Entities;
using Services.Interfaces;
using DAL;
using System.Security.Claims;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly IInMemorySchoolStore _store;
        private readonly ApplicationDbContext _context;

        public TeachersController(IInMemorySchoolStore store, ApplicationDbContext context)
        {
            _store = store;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                // Read UserId and SchoolId from JWT claims
                var userIdClaim = User?.Claims?.FirstOrDefault(c => c.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase));
                var schoolIdClaim = User?.Claims?.FirstOrDefault(c => c.Type.Equals("SchoolId", StringComparison.OrdinalIgnoreCase));

                // Parse UserId to determine role
                // UserRole 1 = SuperAdmin, UserRole 2 = SchoolAdmin
                int? userId = null;
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int parsedUserId))
                {
                    userId = parsedUserId;
                }

                // Parse SchoolId from claims
                int? schoolId = null;
                if (schoolIdClaim != null && int.TryParse(schoolIdClaim.Value, out int parsedSchoolId))
                {
                    schoolId = parsedSchoolId;
                }

                // Query teachers from database
                var query = _context.Teachers.AsQueryable();

                // Role-based filtering
                // SuperAdmin (UserId = 1) → return all teachers
                // SchoolAdmin (UserId = 2) → filter by SchoolId from token
                if (!userId.HasValue)
                {
                    return Unauthorized(new ApiResponse<List<TeacherDto>>
                    {
                        Success = false,
                        Message = "UserId claim not found in token",
                        Data = null
                    });
                }

                if (userId.Value == 2) // SchoolAdmin
                {
                    if (schoolId.HasValue && schoolId.Value > 0)
                    {
                        query = query.Where(t => t.SchoolId == schoolId.Value);
                    }
                    else
                    {
                        // SchoolAdmin must have SchoolId in token
                        return BadRequest(new ApiResponse<List<TeacherDto>>
                        {
                            Success = false,
                            Message = "SchoolId is required in token for SchoolAdmin role",
                            Data = null
                        });
                    }
                }
                // SuperAdmin (userId == 1) → no filtering, return all teachers

                var teachers = await query
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => new TeacherDto
                    {
                        Id = t.Id,
                        Name = $"{t.FirstName} {t.LastName}".Trim()
                    })
                    .ToListAsync();

                string message = userId.HasValue && userId.Value == 2
                    ? $"Teachers retrieved successfully for SchoolId: {schoolId.Value}"
                    : "Teachers retrieved successfully (all schools - SuperAdmin)";

                return Ok(new ApiResponse<List<TeacherDto>>
                {
                    Success = true,
                    Message = message,
                    Data = teachers
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<TeacherDto>>
                {
                    Success = false,
                    Message = $"Error retrieving teachers: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Name is required",
                    Data = null
                });
            }

            var teacherDto = new TeacherDto
            {
                Name = request.Name
            };

            var createdTeacher = _store.AddTeacher(teacherDto);

            return Ok(new ApiResponse<TeacherDto>
            {
                Success = true,
                Message = "Teacher created successfully",
                Data = createdTeacher
            });
        }
    }
}

