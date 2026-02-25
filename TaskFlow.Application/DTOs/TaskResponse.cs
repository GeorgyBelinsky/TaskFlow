using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
