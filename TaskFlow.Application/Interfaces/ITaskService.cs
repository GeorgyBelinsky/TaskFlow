using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateAsync(Guid projectId,CreateTaskRequest request,CancellationToken cancellationToken);
        Task<TaskResponse> UpdateAsync(Guid taskId, UpdateTaskRequest request, CancellationToken cancellationToken);
        Task<Guid> DeleteAsync (Guid taskId, CancellationToken cancellationToken);
    }
}
