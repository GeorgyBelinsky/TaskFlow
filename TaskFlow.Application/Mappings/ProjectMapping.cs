using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mappings
{
    public static class ProjectMapping
    {
        public static Project ToEntity(
            this CreateProjectRequest dto)
        {
            return new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ImageUrl = null,
                CreatedAt = DateTime.UtcNow,
                Tasks = []
            };
        }
        public static ProjectResponse ToResponse(this Project entity)
        {
            return new ProjectResponse
            (
                entity.Id,
                entity.ImageUrl,
                entity.Name
            );
        }
    }
}
