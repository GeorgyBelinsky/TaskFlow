using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class ProjectMember
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ProjectRole ProjectRole { get; set; } = ProjectRole.Member;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
