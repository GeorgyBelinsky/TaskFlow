using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
