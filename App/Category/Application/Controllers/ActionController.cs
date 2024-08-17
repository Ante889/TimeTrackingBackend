using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TimeTracking.App.Phase.Application.Query;
using TimeTracking.App.Project.Application.Query;
using TimeTracking.App.Phase.Application.Command;
using TimeTracking.App.Phase.Domain.Model;
using TimeTracking.App.Project.Domain.Model;
using TimeTracking.App.Category.Domain.Model;

namespace TimeTracking.App.Phase.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _mediator.Send(new FindCategoryByIdQuery(categoryId));

            if (category == null) return NotFound("No category found with the given ID.");

            return Ok(category);
        }

        [HttpGet("category/{phaseId}")]
        public async Task<IActionResult> GetCategoriesByPhaseId(string phaseId)
        {
            var phase = await _mediator.Send(new FindPhaseByIdQuery(int.Parse(phaseId)));
            if (phase == null) return NotFound("No phase found for the given ID.");

            var categories = await _mediator.Send(new FindCategoriesByPhaseQuery(phase));
            if (categories == null || !categories.Any()) return NotFound("No categories found for the given phase.");

            return Ok(categories);
        }

        [HttpPost("cateroy")]
        public async Task<IActionResult> CreatePhase([FromBody] CategoryModel formModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _mediator.Send(new FindPhaseByIdQuery(formModel.Phase));
            if (category == null) return NotFound("No category found for the given ID.");

            var category = await _mediator.Send(new CreateCommand(
                
            ));

            if (category == null) return BadRequest("Failed to create the category.");

            return CreatedAtAction(nameof(GetCategoryById), new { CategoryId = category.Id }, category);
        }

        [HttpPut("category/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryUpdateModel formModel)
        {
            var updateCategory = await _mediator.Send(new UpdateCommand(
         
            ));

            if (updateCategory == null) return NotFound("Category not found or update failed.");

            return Ok(updateCategory);
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
