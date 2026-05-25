using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Services.Interfaces;

namespace Services.Methods
{
    public class CommonService: ICommonService
    {
        private readonly JWTOptions _jwtsecretOptions;
        public CommonService(IOptionsMonitor<JWTOptions> options)
        {

            _jwtsecretOptions = options.CurrentValue;
        }
        public async Task<JwtTokenModel> GenerateTokenAsync(int userId, int roleId, int? schoolId = null)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim("UserId", userId.ToString()),
                    new Claim("RoleId", roleId.ToString()),
                };

                if (schoolId.HasValue && schoolId.Value > 0)
                {
                    claims.Add(new Claim("SchoolId", schoolId.Value.ToString()));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    TokenType = "Bearer",
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtsecretOptions.TimeoutInMins),
                    SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsecretOptions.SecretKey)),
                            SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return new JwtTokenModel()
                {
                    Token = tokenHandler.WriteToken(token),
                    TokenType = "Bearer",
                    ExpiersIn = _jwtsecretOptions.TimeoutInMins
                };
            }
            catch (Exception) { throw; }
        }
    }
}
