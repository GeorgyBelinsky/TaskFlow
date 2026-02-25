using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;
using Task = TaskFlow.Domain.Entities.Task;
namespace TaskFlow.Application.Mappings
{
    public static class TaskMapping
    {
        public static Task ToEntity(
            this CreateTaskRequest dto,
            Guid projectId)
        {
            return new Task
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = TaskStatus.Todo,
                CreatedAt = DateTime.UtcNow,
                ProjectId = projectId
            };
        }

        public static TaskResponse ToResponse(this Task entity)
        {
            return new TaskResponse
            (
                entity.Id,
                entity.Status,
                entity.Priority,
                entity.Title
            );
        }
    }
}
