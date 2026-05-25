using System.Security.Claims;
using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace EduConnect.Helpers
{
    /// <summary>
    /// Role 1 = SuperAdmin, Role 2 = School Admin
    /// </summary>
    public sealed class AuthenticatedUserContext
    {
        public int UserId { get; init; }
        public int Role { get; init; }
        public int? SchoolId { get; init; }
        public bool IsSuperAdmin => Role == 1;
        public bool IsSchoolAdmin => Role == 2;
    }

    public static class SchoolAccessHelper
    {
        public const int SuperAdminRole = 1;
        public const int SchoolAdminRole = 2;

        public static AuthenticatedUserContext? FromClaims(ClaimsPrincipal? principal)
        {
            if (principal?.Identity?.IsAuthenticated != true)
                return null;

            var userIdClaim = FindClaimValue(principal, "UserId", "userId", "user_id", ClaimTypes.NameIdentifier, "sub");
            var roleClaim = FindClaimValue(principal, "RoleId", "roleId", "role_id", ClaimTypes.Role, "role", "UserRole", "userRole");

            if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
                return null;

            if (!int.TryParse(roleClaim, out var role) || role <= 0)
                return null;

            int? schoolId = null;
            var schoolIdClaim = FindClaimValue(principal, "SchoolId", "schoolId", "school_id");
            if (int.TryParse(schoolIdClaim, out var parsedSchoolId) && parsedSchoolId > 0)
                schoolId = parsedSchoolId;

            return new AuthenticatedUserContext
            {
                UserId = userId,
                Role = role,
                SchoolId = schoolId,
            };
        }

        private static string? FindClaimValue(ClaimsPrincipal principal, params string[] claimTypes)
        {
            foreach (var claimType in claimTypes)
            {
                var value = principal.FindFirst(claimType)?.Value;
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }
            return null;
        }

        /// <summary>
        /// Resolves the school linked to a school-admin user (email matches schools.email).
        /// </summary>
        public static async Task<int?> ResolveSchoolIdForUserAsync(
            ApplicationDbContext context,
            User user,
            CancellationToken cancellationToken = default)
        {
            if (user.Role != SchoolAdminRole)
                return null;

            if (string.IsNullOrWhiteSpace(user.UserName))
                return null;

            var normalizedUserName = user.UserName.Trim().ToLowerInvariant();

            var school = await context.Schools
                .AsNoTracking()
                .Where(s => s.Email != null && s.Email.ToLower() == normalizedUserName)
                .Select(s => new { s.Id })
                .FirstOrDefaultAsync(cancellationToken);

            return school?.Id;
        }

        public static async Task<AuthenticatedUserContext?> ResolveAsync(
            HttpContext httpContext,
            ApplicationDbContext context,
            CancellationToken cancellationToken = default)
        {
            var principal = httpContext.User;

            // Ensure JWT is validated even when endpoint has no [Authorize] attribute
            if (principal?.Identity?.IsAuthenticated != true)
            {
                var authResult = await httpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                if (authResult?.Succeeded == true && authResult.Principal != null)
                {
                    principal = authResult.Principal;
                    httpContext.User = principal;
                }
            }

            return await ResolveAsync(principal, context, cancellationToken);
        }

        public static async Task<AuthenticatedUserContext?> ResolveAsync(
            ClaimsPrincipal? principal,
            ApplicationDbContext context,
            CancellationToken cancellationToken = default)
        {
            var auth = FromClaims(principal);
            if (auth == null)
                return null;

            if (auth.SchoolId.HasValue || !auth.IsSchoolAdmin)
                return auth;

            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == auth.UserId, cancellationToken);

            if (user == null)
                return auth;

            var schoolId = await ResolveSchoolIdForUserAsync(context, user, cancellationToken);
            if (!schoolId.HasValue)
                return auth;

            return new AuthenticatedUserContext
            {
                UserId = auth.UserId,
                Role = auth.Role,
                SchoolId = schoolId,
            };
        }

        /// <summary>
        /// Validates and returns the school id that may be written for the current user.
        /// </summary>
        public static async Task<(bool Success, int SchoolId, string? ErrorMessage, int StatusCode)> ResolveWritableSchoolIdAsync(
            AuthenticatedUserContext auth,
            ApplicationDbContext context,
            int requestedSchoolId,
            CancellationToken cancellationToken = default)
        {
            if (auth.IsSchoolAdmin)
            {
                if (!auth.SchoolId.HasValue)
                {
                    return (false, 0,
                        "Your account is not linked to a school. Use the same email as the school registration.",
                        StatusCodes.Status400BadRequest);
                }

                if (requestedSchoolId > 0 && requestedSchoolId != auth.SchoolId.Value)
                {
                    return (false, 0,
                        "You can only manage students for your own school.",
                        StatusCodes.Status403Forbidden);
                }

                return (true, auth.SchoolId.Value, null, StatusCodes.Status200OK);
            }

            if (auth.IsSuperAdmin)
            {
                if (requestedSchoolId <= 0)
                {
                    return (false, 0, "SchoolId is required.", StatusCodes.Status400BadRequest);
                }

                var schoolExists = await context.Schools
                    .AsNoTracking()
                    .AnyAsync(s => s.Id == requestedSchoolId, cancellationToken);

                if (!schoolExists)
                {
                    return (false, 0, "School not found.", StatusCodes.Status404NotFound);
                }

                return (true, requestedSchoolId, null, StatusCodes.Status200OK);
            }

            return (false, 0, "You are not allowed to perform this action.", StatusCodes.Status403Forbidden);
        }

        /// <summary>
        /// Resolves which school id to use when reading students.
        /// </summary>
        public static async Task<(bool Success, int SchoolId, string? ErrorMessage, int StatusCode)> ResolveReadableSchoolIdAsync(
            AuthenticatedUserContext auth,
            ApplicationDbContext context,
            int requestedSchoolId,
            CancellationToken cancellationToken = default)
        {
            if (auth.IsSchoolAdmin)
            {
                if (!auth.SchoolId.HasValue)
                {
                    return (false, 0,
                        "Your account is not linked to a school.",
                        StatusCodes.Status400BadRequest);
                }

                if (requestedSchoolId > 0 && requestedSchoolId != auth.SchoolId.Value)
                {
                    return (false, 0,
                        "You can only view students for your own school.",
                        StatusCodes.Status403Forbidden);
                }

                return (true, auth.SchoolId.Value, null, StatusCodes.Status200OK);
            }

            if (auth.IsSuperAdmin)
            {
                if (requestedSchoolId <= 0)
                {
                    return (false, 0, "SchoolId is required.", StatusCodes.Status400BadRequest);
                }

                var schoolExists = await context.Schools
                    .AsNoTracking()
                    .AnyAsync(s => s.Id == requestedSchoolId, cancellationToken);

                if (!schoolExists)
                {
                    return (false, 0, "School not found.", StatusCodes.Status404NotFound);
                }

                return (true, requestedSchoolId, null, StatusCodes.Status200OK);
            }

            return (false, 0, "You are not allowed to perform this action.", StatusCodes.Status403Forbidden);
        }
    }
}
