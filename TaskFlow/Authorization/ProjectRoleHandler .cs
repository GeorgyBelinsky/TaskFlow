using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace TaskFlow.Infrastructure.Services
{
    public class ProjectRoleHandler: AuthorizationHandler<ProjectRoleRequirement>
    {
        public readonly AppDbContext _db;
        public ProjectRoleHandler(AppDbContext db)
        {
            _db = db; 
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ProjectRoleRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return;

            string? projectIdRaw = null;

            if (context.Resource is HttpContext httpContext)
            {
                projectIdRaw = httpContext.GetRouteValue("projectId")?.ToString();
            }

            if (!Guid.TryParse(projectIdRaw, out var projectId))
                return;

            var userId = Guid.Parse(userIdClaim);

            var role = await _db.ProjectMembers
                .Where(pm => pm.ProjectId == projectId && pm.UserId == userId)
                .Select(pm => pm.ProjectRole)
                .FirstOrDefaultAsync();

            if (requirement.AllowedRoles.Contains(role))
            {
                context.Succeed(requirement);
            }
        }
    }
}
