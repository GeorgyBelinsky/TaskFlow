using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs
{
    public record CreateTaskRequest
    (
         string Title ,
         string? Description,
         TaskPriority Priority 
    );
}
