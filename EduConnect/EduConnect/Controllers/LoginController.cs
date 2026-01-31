using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class LoginController : ControllerBase
    {
        public ICommonService _commonService { get; set; }
        private readonly ApplicationDbContext _context;
        public LoginController(ICommonService commonService, ApplicationDbContext context) {
            _commonService = commonService; 
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var user = _context.Users.FirstOrDefault(x=> request.UserName == x.UserName && x.PasswordHash == request.Password);
            if(user != null)
            {
                var result = await _commonService.GenerateTokenAsync(user.Id, user.Role);
                return Ok(result);
            }
            return BadRequest("Login Fail");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAllDetails()
        {
            var userIdClaim = User?.Claims?.FirstOrDefault(c => c.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase));
            var isvalid = int.TryParse(userIdClaim?.Value, out int userId) ? userId : 0;
            if (isvalid != 0)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                var result = new UserModel()
                {
                    UserId = userId,
                    Email = user.UserName,
                    UserRole = user.Role,
                };
                return Ok(result);
            }
            return BadRequest("Bad request");
        }
    }
}