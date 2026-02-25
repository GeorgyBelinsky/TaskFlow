using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResponse> CreateAsync(CreateProjectRequest request,CancellationToken cancellationToken);
        Task<ProjectStatsResponse> GetStatsAsync(Guid projectId,CancellationToken cancellationToken);
        Task UploadImageAsync(Guid projectId,Stream fileStream,string fileName,string contentType,CancellationToken cancellationToken);
        Task<List<ProjectResponse>> GetAllProjectsAsync(CancellationToken cancellationToken);
        Task <List<TaskResponse>> GetProjectTasksAsync(Guid projectId, CancellationToken cancellationToken);
    }
}
