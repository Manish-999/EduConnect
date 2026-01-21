using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs;
using Services.Interfaces;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly IInMemorySchoolStore _store;

        public TeachersController(IInMemorySchoolStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var teachers = _store.GetAllTeachers();

            return Ok(new ApiResponse<List<TeacherDto>>
            {
                Success = true,
                Message = "Teachers retrieved successfully",
                Data = teachers
            });
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

