using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Phase.Application.Query;
using TimeTracking.App.Project.Application.Query;
using TimeTracking.App.Phase.Application.Command;
using TimeTracking.App.Phase.Domain.Model;
using TimeTracking.App.Project.Domain.Model;

namespace TimeTracking.App.Phase.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/")]
    public class PhaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhaseController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("phase/{phaseId}")]
        public async Task<IActionResult> GetPhaseById(int phaseId)
        {
            var phase = await _mediator.Send(new FindPhaseByIdQuery(phaseId));

            if (phase == null) return NotFound("No phase found with the given ID.");

            return Ok(phase);
        }

        [HttpGet("phases/{projectId}")]
        public async Task<IActionResult> GetPhasesByProjectId(string projectId)
        {
            var project = await _mediator.Send(new FindProjectByIdQuery(int.Parse(projectId)));
            if (project == null) return NotFound("No Project found for the given ID.");

            var phases = await _mediator.Send(new FindPhaseByProjectQuery(project));
            if (phases == null || !phases.Any()) return NotFound("No phases found for the given project.");

            return Ok(phases);
        }

        [HttpPost("phase")]
        public async Task<IActionResult> CreatePhase([FromBody] PhaseModel formModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = await _mediator.Send(new FindProjectByIdQuery(formModel.Project));
            if (project == null) return NotFound("No project found for the given ID.");

            var phase = await _mediator.Send(new CreateCommand(
                project,
                formModel.DateCreated,
                formModel.PhaseNumber,
                formModel.Description,
                formModel.AmountPaid
            ));

            if (phase == null) return BadRequest("Failed to create the project.");

            return CreatedAtAction(nameof(GetPhaseById), new { PhaseId = phase.Id }, phase);
        }

        [HttpPut("phase/{phaseId}")]
        public async Task<IActionResult> UpdatePhase(int phaseId, [FromBody] PhaseUpdateModel formModel)
        {
            var updatePhase = await _mediator.Send(new UpdateCommand(
                phaseId,
                formModel.Description,
                formModel.AmountPaid
            ));

            if (updatePhase == null) return NotFound("Phase not found or update failed.");

            return Ok(updatePhase);
        }


        [HttpDelete("phases/{phaseId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var result = await _mediator.Send(new DeleteCommand(projectId));
            if (!result) return NotFound("Phase not found or delete failed.");

            return NoContent();
        }
    }
}
