using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappings;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _db;
        private readonly IProjectAccessService _access;
        public TaskService(AppDbContext db, IProjectAccessService access )
        {
            _db = db;
            _access = access;
        }
        
        public async Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken)
        {
            await _access.EnsureCanEditTask(projectId, cancellationToken);

            var task = request.ToEntity(projectId);

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync(cancellationToken);

            return task.ToResponse();
        }

        public async Task<TaskResponse> UpdateAsync(Guid taskId, UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var projectId = await _db.Tasks.Where(t => t.Id == taskId).Select(t => t.ProjectId).FirstAsync(cancellationToken);
            await _access.EnsureCanEditTask(projectId, cancellationToken);

            var affected = await _db.Tasks
           .Where(t => t.Id == taskId)
           .ExecuteUpdateAsync(t => t
               .SetProperty(t => t.Title, request.Title)
               .SetProperty(t => t.Status, request.Status)
               .SetProperty(t => t.Priority, request.Priority),
               cancellationToken);

            if (affected == 0)
                throw new Exception("Task not found");

            var updatedTask = await _db.Tasks
                .AsNoTracking()
                .FirstAsync(t => t.Id == taskId, cancellationToken);

            return updatedTask.ToResponse();
        }

        public async Task<Guid> DeleteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var projectId = await _db.Tasks.Where(t=>t.Id == taskId).Select(t=>t.ProjectId).FirstAsync(cancellationToken);
            await _access.EnsureCanDeleteTask(projectId, cancellationToken);
            var deleted = await _db.Tasks.Where(t=>t.Id == taskId).ExecuteDeleteAsync(cancellationToken);

            if (deleted == 0)
                throw new Exception("Task not found");

            return taskId;
        }
    }
}
