using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
