namespace TaskFlow.Application.Interfaces
{
    public interface IProjectAccessService
    {
        Task EnsureCanManageProject(Guid projectId, CancellationToken cancellationToken);
        Task EnsureCanEditTask(Guid projectId, CancellationToken cancellationToken);
        Task EnsureCanDeleteTask(Guid projectId, CancellationToken cancellationToken);
        Task EnsureCanViewProject(Guid projectId, CancellationToken cancellationToken);
    }
}
