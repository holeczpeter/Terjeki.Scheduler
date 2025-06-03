using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Terjeki.Scheduler.Core
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserId()
        {
            var claim = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (claim != null && Guid.TryParse(claim, out var id))
                return id;

            return null;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?
                .User
                .Identity?
                .Name
                ?? string.Empty;
        }
        public string GetUserRole()
        {
            
            var roleClaim = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.Role)?.Value;

            return roleClaim ?? string.Empty;
        }
    }
}
