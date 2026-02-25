using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        [HttpPost("{projectId}/create")]
        public async Task<IActionResult> Create(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var result = await _taskService.CreateAsync(projectId, request, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{taskId}/edit")]
        public async Task<IActionResult> Update(Guid taskId, UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var result = await _taskService.UpdateAsync(taskId, request, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{taskId}/delete")]
        public async Task<IActionResult> Delete(Guid taskId, CancellationToken cancellationToken)
        {
            await _taskService.DeleteAsync(taskId, cancellationToken);
            return NoContent();
        }
    }
}
