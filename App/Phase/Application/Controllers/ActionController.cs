using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Phase.Application.Query;
using TimeTracking.App.Project.Application.Query;
using TimeTracking.App.Phase.Application.Command;
using TimeTracking.App.Phase.Domain.Model;
using TimeTracking.App.Base.Controllers;

namespace TimeTracking.App.Phase.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/")]
    public class PhaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SafeExecutorInterface _safeExecutor;

        public PhaseController(
            IMediator mediator,
            SafeExecutorInterface safeExecutor
        )
        {
            _mediator = mediator;
            _safeExecutor = safeExecutor;
        }

        [HttpGet("phase/{phaseId}")]
        public Task<IActionResult> GetPhaseById(int phaseId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var phase = await _mediator.Send(new FindPhaseByIdQuery(phaseId));

                if (phase == null) return NotFound("No phase found with the given ID.");

                return Ok(phase);
            });
        }

        [HttpGet("phases/{projectId}")]
        public Task<IActionResult> GetPhasesByProjectId(string projectId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var project = await _mediator.Send(new FindProjectByIdQuery(int.Parse(projectId)));
                if (project == null) return NotFound("No Project found for the given ID.");

                var phases = await _mediator.Send(new FindPhaseByProjectQuery(project));
                if (phases == null || !phases.Any()) return NotFound("No phases found for the given project.");

                return Ok(phases);
            });
        }

        [HttpPost("phase")]
        public Task<IActionResult> CreatePhase([FromBody] PhaseModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
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
            });
        }

        [HttpPut("phase/{phaseId}")]
        public Task<IActionResult> UpdatePhase(int phaseId, [FromBody] PhaseUpdateModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var updatePhase = await _mediator.Send(new UpdateCommand(
                    phaseId,
                    formModel.Description,
                    formModel.AmountPaid
                ));

                if (updatePhase == null) return NotFound("Phase not found or update failed.");

                return Ok(updatePhase);
            });
        }

        [HttpDelete("phase/{phaseId}")]
        public Task<IActionResult> DeletePhase(int phaseId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var result = await _mediator.Send(new DeleteCommand(phaseId));
                if (!result) return NotFound("Phase not found or delete failed.");

                return NoContent();
            });
        }
    }
}
