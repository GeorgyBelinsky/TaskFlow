using Microsoft.Extensions.Logging;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Infrastructure.Decorators
{
    internal class LoggingTaskService : ITaskService
    {
        private readonly ITaskService _inner;
        private readonly ILogger<LoggingTaskService> _logger;

        public LoggingTaskService(
      ITaskService inner,
      ILogger<LoggingTaskService> logger)
        {
            _inner = inner;
            _logger = logger;
        }
        public async Task<TaskResponse> CreateAsync(
    Guid projectId,
    CreateTaskRequest request,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Creating task in project {ProjectId} with title {Title}",
                projectId,
                request.Title);

            var result = await _inner.CreateAsync(projectId, request, cancellationToken);

            _logger.LogInformation(
                "Task created with id {TaskId}",
                result.Id);

            return result;
        }

        public async Task<TaskResponse> UpdateAsync(
    Guid taskId,
    UpdateTaskRequest request,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Updating task {TaskId}",
                taskId);

            var result = await _inner.UpdateAsync(taskId, request, cancellationToken);

            _logger.LogInformation(
                "Task {TaskId} updated",
                taskId);

            return result;
        }

        public async Task<Guid> DeleteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Deleting task {TaskId}",
                taskId);

            var id = await _inner.DeleteAsync(taskId, cancellationToken);

            _logger.LogInformation(
                "Task {TaskId} deleted",
                id);

            return id;
        }
    }
}
