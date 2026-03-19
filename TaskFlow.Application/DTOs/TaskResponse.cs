using TaskFlow.Domain.Enums;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.DTOs
{
    public record TaskResponse
    (
         Guid Id,
         TaskStatus Status,
         TaskPriority Priority,
         string Title = default!
    );
}
