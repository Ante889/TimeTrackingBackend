using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Project.Application.Query;
using TimeTracking.App.Person.Application.Query;
using TimeTracking.App.Project.Domain.Model;
using TimeTracking.App.Project.Application.Command;
using TimeTracking.App.Base.Controllers;

namespace TimeTracking.App.Project.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SafeExecutorInterface _safeExecutor;

        public ProjectController(
            IMediator mediator,
            SafeExecutorInterface safeExecutor
        )
        {
            _mediator = mediator;
            _safeExecutor = safeExecutor;
        }

        [HttpGet("project/{projectId}")]
        public Task<IActionResult> GetProjectById(int projectId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var project = await _mediator.Send(new FindProjectByIdQuery(projectId));

                if (project == null) return NotFound("No project found with the given ID.");

                return Ok(project);
            });
        }

        [HttpGet("projects/{userId}")]
        public Task<IActionResult> GetProjectsByUserId(string userId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var person = await _mediator.Send(new FindPersonByIdQuery(userId));
                if (person == null) return NotFound("No person found for the given ID.");

                var projects = await _mediator.Send(new FindProjectByUserQuery(person));

                return Ok(projects);
            });
        }

        [HttpPost("project")]
        public Task<IActionResult> CreateProject([FromBody] ProjectModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var person = await _mediator.Send(new FindPersonByIdQuery(formModel.UserCreated.ToString()));
                if (person == null) return NotFound("No person found for the given ID.");

                var project = await _mediator.Send(new CreateCommand(
                    person,
                    formModel.DateCreated,
                    formModel.Name,
                    formModel.Description
                ));

                if (project == null) return BadRequest("Failed to create the project.");

                return CreatedAtAction(nameof(GetProjectById), new { projectId = project.Id }, project);
            });
        }

        [HttpPut("project/{projectId}")]
        public Task<IActionResult> UpdateProject(int projectId, [FromBody] ProjectUpdateModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var updatedProject = await _mediator.Send(new UpdateCommand(
                    projectId,
                    formModel.DateCreated,
                    formModel.Name,
                    formModel.Description
                ));

                if (updatedProject == null) return NotFound("Project not found or update failed.");

                return Ok(updatedProject);
            });
        }

        [HttpDelete("project/{projectId}")]
        public Task<IActionResult> DeleteProject(int projectId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var result = await _mediator.Send(new DeleteCommand(projectId));
                if (!result) return NotFound("Project not found or delete failed.");

                return NoContent();
            });
        }
    }
}
