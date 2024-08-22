using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Phase.Application.Query;
using TimeTracking.App.Category.Domain.Model;
using TimeTracking.App.Category.Application.Query;
using TimeTracking.App.Category.Application.Command;
using TimeTracking.App.Base.Controllers;

namespace TimeTracking.App.Category.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SafeExecutorInterface _safeExecutor;

        public CategoryController(
            IMediator mediator,
            SafeExecutorInterface safeExecutor 
        )
        {
            _mediator = mediator;
            _safeExecutor = safeExecutor;
        }

        [HttpGet("category/{categoryId}")]
        public Task<IActionResult> GetCategoryById(int categoryId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var category = await _mediator.Send(new FindCategoryByIdQuery(categoryId));

                if (category == null) return NotFound("No category found with the given ID.");

                return Ok(category);
            });
        }

        [HttpGet("categories/{phaseId}")]
        public Task<IActionResult> GetCategoriesByPhaseId(string phaseId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var phase = await _mediator.Send(new FindPhaseByIdQuery(int.Parse(phaseId)));
                if (phase == null) return NotFound("No phase found for the given ID.");

                var categories = await _mediator.Send(new FindCategoriesByPhaseQuery(phase));
                if (categories == null || !categories.Any()) return NotFound("No categories found for the given phase.");

                return Ok(categories);
            });
        }

        [HttpPost("category")]
        public Task<IActionResult> CreateCategory([FromBody] CategoryModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var phase = await _mediator.Send(new FindPhaseByIdQuery(formModel.Phase));
                if (phase == null) return NotFound("No phase found for the given ID.");

                var category = await _mediator.Send(new CreateCommand(
                    phase,
                    formModel.Name,
                    formModel.PricePerHour
                ));

                if (category == null) return BadRequest("Failed to create the category.");

                return CreatedAtAction(nameof(GetCategoryById), new { CategoryId = category.Id }, category);
            });
        }

        [HttpPut("category/{categoryId}")]
        public Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryUpdateModel formModel)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var updateCategory = await _mediator.Send(new UpdateCommand(
                    categoryId,
                    formModel.Name,
                    formModel.PricePerHour
                ));

                if (updateCategory == null) return NotFound("Category not found or update failed.");

                return Ok(updateCategory);
            });
        }

        [HttpDelete("category/{categoryId}")]
        public Task<IActionResult> DeleteCategory(int categoryId)
        {
            return _safeExecutor.ExecuteSafe(async () =>
            {
                var result = await _mediator.Send(new DeleteCommand(categoryId));
                if (!result) return NotFound("Category not found or delete failed.");

                return NoContent();
            });
        }
    }
}
