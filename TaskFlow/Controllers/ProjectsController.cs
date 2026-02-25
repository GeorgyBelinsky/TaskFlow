using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [Authorize]
        [HttpPost("/create")]
        public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var result = await _projectService.CreateAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{projectId}/stats")]
        public async Task<ActionResult<ProjectStatsResponse>> GetStats(Guid projectId,CancellationToken cancellationToken)
        {
            return Ok(await _projectService.GetStatsAsync(projectId, cancellationToken));
        }

        [Authorize]
        [HttpPost("{projectId}/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(
            Guid projectId,
            [FromForm] UploadProjectImageRequest request,
            CancellationToken cancellationToken)
        {

            if (!request.Image.ContentType.StartsWith("image/"))
                throw new Exception("Only images allowed");

            if (request.Image.Length > 5_000_000)
                throw new Exception("Max 5MB");

            await _projectService.UploadImageAsync(
            projectId,
            request.Image.OpenReadStream(),
            request.Image.FileName,
            request.Image.ContentType,
            cancellationToken);
            
            return NoContent();
        }

        [HttpGet("/projects")]
        public async Task<ActionResult<List<ProjectResponse>>> GetAllProjects(CancellationToken cancellationToken)
        {
            return Ok(await _projectService.GetAllProjectsAsync(cancellationToken));
        }

        [HttpGet("{projectId}/tasks")]
        public async Task<ActionResult<List<TaskResponse>>> GetProjectTasksAsync (Guid projectId, CancellationToken cancellationToken)
        {
            return Ok(await _projectService.GetProjectTasksAsync(projectId, cancellationToken));
        }
    }
}
