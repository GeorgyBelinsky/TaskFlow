using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappings;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Infrastructure.Data;
using Task = System.Threading.Tasks.Task;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _db;
        private readonly ICurrentUserService _currentUser;

        //private readonly IImageStorage _imageStorage;
        public ProjectService(AppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;

            //_imageStorage = imageStorage;
        }

        public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken)
        {

            var project = request.ToEntity();
            
            project.Members.Add(new ProjectMember
            {
                UserId = _currentUser.UserId,
                ProjectRole = ProjectRole.Owner
            });
            _db.Projects.Add(project);
            await _db.SaveChangesAsync(cancellationToken);

            return project.ToResponse();
        }

        public async Task<ProjectStatsResponse> GetStatsAsync(
       Guid projectId,
       CancellationToken cancellationToken)
        {
            var stats = await _db.Tasks
                .Where(t => t.ProjectId == projectId)
                .GroupBy(_ => 1)
                .Select(g => new ProjectStatsResponse
                (
                     g.Count(),
                     g.Count(t => t.Status == TaskStatus.Todo),
                     g.Count(t => t.Status == TaskStatus.InProgress),
                     g.Count(t => t.Status == TaskStatus.Done)
                ))
                .FirstOrDefaultAsync(cancellationToken);

            return stats ?? new ProjectStatsResponse(0,0,0,0);
        }
        public async Task UploadImageAsync(Guid projectId,Stream fileStream,string fileName,string contentType,CancellationToken cancellationToken)
        {
            var project = await _db.Projects.FindAsync(projectId);
            if (project is null)
                throw new Exception("Project not found");

            //var url = await _imageStorage.UploadAsync(
            //    fileStream,
            //    fileName,
            //    contentType,
            //    cancellationToken
            //    );

            //project.ImageUrl = url;
            //await _db.SaveChangesAsync(cancellationToken);
            var ext = Path.GetExtension(fileName);
            var imageName = $"{projectId}{ext}";
            var path = Path.Combine("wwwroot", "projects", imageName);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            await using var fs = new FileStream(path, FileMode.Create);
            await fileStream.CopyToAsync(fs, cancellationToken);

            project.ImageUrl = $"/projects/{imageName}";
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ProjectResponse>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            return await _db.Projects.AsNoTracking().Select(p=>p.ToResponse()).ToListAsync(cancellationToken);
        }

        public async Task<List<TaskResponse>> GetProjectTasksAsync(Guid projectId, CancellationToken cancellationToken)
        {
            return await _db.Tasks
                .Where(t => t.ProjectId == projectId)
                .Select(p => p.ToResponse())
                .ToListAsync(cancellationToken);
        }
    }
}
