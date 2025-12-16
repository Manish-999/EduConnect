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
            if (request.UserName == "Admin") {
                if (request.Password == "Password") {
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
            var result = new UserModel()
            {
                UserId = int.TryParse(userIdClaim?.Value, out int userId)?userId:0,
                Email = "Admin@gmail.com",
                UserRole = 1
            };
            return BadRequest(result);
        }
    }
}
