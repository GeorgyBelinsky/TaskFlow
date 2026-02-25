using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
