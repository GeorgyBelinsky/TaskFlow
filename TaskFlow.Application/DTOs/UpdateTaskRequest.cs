using TaskFlow.Domain.Enums;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.DTOs
{
    public record UpdateTaskRequest
    (
         TaskStatus Status,
         TaskPriority Priority,
         string Title = default!
    );
}
