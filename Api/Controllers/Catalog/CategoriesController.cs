using Application.Features.Catalogs.Categories.Commands;
using Application.Features.Catalogs.Categories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class CategoriesController : BaseApiController<CategoriesController>
    {
        [Authorize(Policy = Permissions.Categories.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }

        [Authorize(Policy = Permissions.Categories.View)]
        [HttpGet("catalog/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForPartner()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesForPartnerQuery()));
        }

        [Authorize(Policy = Permissions.Categories.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Categories.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(CategoryRequest request)
        {
            return Ok(await _mediator.Send(new AddEditCategoryCommand() { CategoryRequest = request }));
        }

        [Authorize(Policy = Permissions.Categories.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand() { Id = id }));
        }
    }
}
