using Microsoft.AspNetCore.Authorization;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Infrastructure.Security
{
    public class ProjectRoleRequirement: IAuthorizationRequirement
    {
        public ProjectRole[] AllowedRoles { get; }

        public ProjectRoleRequirement(params ProjectRole[] roles)
        {
            AllowedRoles = roles;
        }
    }
}
