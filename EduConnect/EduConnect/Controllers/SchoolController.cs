using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("tc/[action]")]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        public ISchoolService _schoolService { get; set; }
        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveSchool([FromBody] SchoolRegistrationRequest request)
        {
            var result = await _schoolService.SaveSchool(request);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> SaveTeacher([FromBody] TeacherRegistrationRequest request)
        {
            var result = await _schoolService.SaveTeacher(request);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> SaveStudent([FromBody] StudentRegistrationRequest request)
        {
            var result = await _schoolService.SaveStudent(request);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllSchool()
        {
            var result = await _schoolService.GetAllSchool();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllTeacher()
        {
            var result = await _schoolService.GetAllTeacher();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllStudent()
        {
            var result = await _schoolService.GetAllStudent();
            return Ok(result);
        }
    }
}
