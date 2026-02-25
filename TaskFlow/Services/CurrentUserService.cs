using TaskFlow.Application.Interfaces;

namespace TaskFlow.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId { 
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                return userId is null ? Guid.Empty: Guid.Parse(userId);  
            }
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
