using DAL;
using EduConnect.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities;
using Services.Interfaces;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class LoginController : ControllerBase
    {
        public ICommonService _commonService { get; set; }
        private readonly ApplicationDbContext _context;

        public LoginController(ICommonService commonService, ApplicationDbContext context)
        {
            _commonService = commonService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var user = _context.Users.FirstOrDefault(x =>
                request.UserName == x.UserName && x.PasswordHash == request.Password);

            if (user == null)
                return BadRequest("Login Fail");

            int? schoolId = null;
            School? school = null;

            if (user.Role == SchoolAccessHelper.SchoolAdminRole)
            {
                schoolId = await SchoolAccessHelper.ResolveSchoolIdForUserAsync(_context, user);
                if (schoolId.HasValue)
                {
                    school = await _context.Schools
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == schoolId.Value);
                }
            }

            var result = await _commonService.GenerateTokenAsync(user.Id, user.Role, schoolId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAllDetails()
        {
            var auth = await SchoolAccessHelper.ResolveAsync(HttpContext, _context);
            if (auth == null)
                return Unauthorized(new { message = "Invalid or missing authentication." });

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == auth.UserId);

            if (user == null)
                return BadRequest(new { message = "User not found." });

            School? school = null;
            if (auth.SchoolId.HasValue)
            {
                school = await _context.Schools
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == auth.SchoolId.Value);
            }

            var result = new UserModel
            {
                UserId = auth.UserId,
                Email = user.UserName,
                UserRole = user.Role,
                SchoolId = auth.SchoolId,
                School = school,
            };

            return Ok(result);
        }
    }
}
