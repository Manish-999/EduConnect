using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public ICommonService _commonService { get; set; }
        public LoginController(ICommonService commonService) {
            _commonService = commonService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (request.UserName == "admin@yopmail.com") {
                if (request.Password == "Password") {
                    var result = await _commonService.GenerateTokenAsync(request.UserName, 2);
                    return Ok(result);
                }
            }
            if (request.UserName == "SuperAdmin@yopmail.com")
            {
                if (request.Password == "Password")
                {
                    var result = await _commonService.GenerateTokenAsync(request.UserName, 1);
                    return Ok(result);
                }
            }
            return BadRequest("Login Fail");
        }
        [HttpPost]
        [Route("GetAllDetails")]
        [Authorize]
        public async Task<IActionResult> GetAllDetails()
        {
            var userIdClaim = User?.Claims?.FirstOrDefault(c => c.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase));
            var userEmail = int.TryParse(userIdClaim?.Value, out int userId) ? (userId == 2) ? "Admin@yopmail.com" : "SuperAdmin@yopmail.com" : "";
            var result = new UserModel()
            {
                UserId = userId,
                Email = userEmail,
                UserRole = userId
            };
            return BadRequest(result);
        }
    }
}
