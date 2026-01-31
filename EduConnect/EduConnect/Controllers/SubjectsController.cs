//using DAL;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Model;
//using Model.DTOs;
//using Services.Interfaces;

//namespace EduConnect.Controllers
//{
//    [ApiController]
//    [Route("api/subjects")]
//    public class SubjectsController : ControllerBase
//    {
//        private readonly IInMemorySchoolStore _store;
//        private readonly ApplicationDbContext _context;
//        public SubjectsController(IInMemorySchoolStore store, ApplicationDbContext context)
//        {
//            _store = store;
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult GetAllSubjects()
//        {
//            var subjects = _store.GetAllSubjects();

//            return Ok(new ApiResponse<List<SubjectDto>>
//            {
//                Success = true,
//                Message = "Subjects retrieved successfully",
//                Data = subjects
//            });
//        }

//        [HttpPost]
//        public IActionResult CreateSubject([FromBody] CreateSubjectRequest request)
//        {
//            if (string.IsNullOrWhiteSpace(request.Name))
//            {
//                return BadRequest(new ApiResponse<object>
//                {
//                    Success = false,
//                    Message = "Subject name is required",
//                    Data = null
//                });
//            }

//            // Check if subject with same name already exists
//            var existingSubjects = _store.GetAllSubjects();
//            if (existingSubjects.Any(s => s.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
//            {
//                return BadRequest(new ApiResponse<object>
//                {
//                    Success = false,
//                    Message = $"Subject with name '{request.Name}' already exists",
//                    Data = null
//                });
//            }

//            var subjectDto = new SubjectDto
//            {
//                Name = request.Name.Trim()
//            };

//            var createdSubject = _store.AddSubject(subjectDto);

//            return Ok(new ApiResponse<SubjectDto>
//            {
//                Success = true,
//                Message = "Subject created successfully",
//                Data = createdSubject
//            });
//        }
//    }
//}

