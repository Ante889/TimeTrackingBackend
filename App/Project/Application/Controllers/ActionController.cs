using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Project.Application.Query;
using TimeTracking.App.Person.Application.Query;
using TimeTracking.App.Project.Domain.Model;
using TimeTracking.App.Project.Application.Command;

namespace TimeTracking.App.Project.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/")]
    public class ActionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActionController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var project = await _mediator.Send(new FindProjectByIdQuery(projectId));
            
            if (project == null) return NotFound("No project found with the given ID.");
            
            return Ok(project);
        }

        [HttpGet("projects/{userId}")]
        public async Task<IActionResult> GetProjectsByUserId(string userId)
        {
            var person = await _mediator.Send(new FindPersonByIdQuery(userId));
            if (person == null) return NotFound("No person found for the given ID.");

            var projects = await _mediator.Send(new FindProjectByUserQuery(person));
            if (projects == null || !projects.Any()) return NotFound("No projects found for the given user.");

            return Ok(projects);
        }

        [HttpPost("project")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectModel formModel)
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
        }

        [HttpPut("project/{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, [FromBody] ProjectUpdateModel formModel)
        {
            var updatedProject = await _mediator.Send(new UpdateCommand(
                projectId,
                formModel.DateCreated,
                formModel.Name,
                formModel.Description
            ));

            if (updatedProject == null) return NotFound("Project not found or update failed.");

            return Ok(updatedProject);
        }

        [HttpDelete("project/{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var result = await _mediator.Send(new DeleteCommand(projectId));
            if (!result) return NotFound("Project not found or delete failed.");

            return NoContent();
        }
    }
}
