using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Enums;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Services
{
    public class ProjectAccessService: IProjectAccessService
    {
        private readonly AppDbContext _db;
        private readonly ICurrentUserService _currentUser;
        public ProjectAccessService(AppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        
        }
       
        public async Task EnsureCanManageProject(Guid projectId, CancellationToken cancellationToken)
        {
            var role = await GetRole(projectId,cancellationToken);
            if (role is not ProjectRole.Owner or ProjectRole.Admin)
                throw new UnauthorizedAccessException("You can not manage this project");
        }

        public async Task EnsureCanEditTask(Guid projectId, CancellationToken cancellationToken)
        {
            var role = await GetRole(projectId, cancellationToken);
            if (role is ProjectRole.Viewer)
                throw new UnauthorizedAccessException("You can not edit tasks");
        }
        public async Task EnsureCanDeleteTask(Guid projectId, CancellationToken cancellationToken)
        {
            var role = await GetRole(projectId, cancellationToken);
            if (role is not ProjectRole.Owner or ProjectRole.Admin)
                throw new UnauthorizedAccessException("You can not delete this task");
        }
        public async Task EnsureCanViewProject(Guid projectId, CancellationToken cancellationToken)
        {
            await GetRole(projectId, cancellationToken);
        }

        private async Task<ProjectRole> GetRole(Guid projectId, CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                throw new UnauthorizedAccessException();

            var role = await _db.ProjectMembers
                .Where(pm => pm.ProjectId == projectId && pm.UserId == _currentUser.UserId)
                .Select(pm => pm.ProjectRole)
                .FirstOrDefaultAsync(ct);

            if (role == default)
                throw new UnauthorizedAccessException("User is not a project member");

            return role;
        }
    }
}
