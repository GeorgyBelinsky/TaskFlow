namespace TaskFlow.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
        public void SetImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}
