using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Base.Controllers;
using TimeTracking.App.Time.Application.Query;
using TimeTracking.App.Time.Application.Command;
using TimeTracking.App.Time.Domain.Model;
using TimeTracking.App.Category.Application.Query;

namespace TimeTracking.App.Time.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/")]
    public class TimeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SafeExecutorInterface _safeExecutor;

        public TimeController(
            IMediator mediator,
            SafeExecutorInterface safeExecutor
        )
        {
            _mediator = mediator;
            _safeExecutor = safeExecutor;
        }

        [HttpGet("time/{timeId}")]
        public Task<IActionResult> GetTimeById(int timeId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var time = await _mediator.Send(new FindTimeByIdQuery(timeId));

                if (time == null) return NotFound("No time entry found with the given ID.");

                return Ok(time);
            });
        }

        [HttpGet("times/{categoryId}")]
        public Task<IActionResult> GetTimesByCategoryId(string categoryId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var category = await _mediator.Send(new FindCategoryByIdQuery(int.Parse(categoryId)));
                if (category == null) return NotFound("No category found for the given ID.");

                var times = await _mediator.Send(new FindTimesByCategoryIdQuery(category));
                if (times == null || !times.Any()) return NotFound("No time entries found for the given category.");

                return Ok(times);
            });
        }

        [HttpPost("time")]
        public Task<IActionResult> CreateTime([FromBody] TimeModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var category = await _mediator.Send(new FindCategoryByIdQuery(formModel.Category));
                if (category == null) return NotFound("No category found for the given ID.");

                var time = await _mediator.Send(new CreateCommand(
                    category,
                    formModel.TimeInMinutes,
                    formModel.Title,
                    formModel.Description
                ));

                if (time == null) return BadRequest("Failed to create the time entry.");

                return CreatedAtAction(nameof(GetTimeById), new { timeId = time.Id }, time);
            });
        }

        [HttpPut("time/{timeId}")]
        public Task<IActionResult> UpdateTime(int timeId, [FromBody] TimeUpdateModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var updatedTime = await _mediator.Send(new UpdateCommand(
                    timeId,
                    formModel.TimeInMinutes,
                    formModel.Title,
                    formModel.Description
                ));

                if (updatedTime == null) return NotFound("Time entry not found or update failed.");

                return Ok(updatedTime);
            });
        }

        [HttpDelete("time/{timeId}")]
        public Task<IActionResult> DeleteTime(int timeId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var result = await _mediator.Send(new DeleteCommand(timeId));
                if (!result) return NotFound("Time entry not found or delete failed.");

                return NoContent();
            });
        }
    }
}
