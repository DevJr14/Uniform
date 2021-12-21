using Application.Features.Catalogs.Tags.Commands;
using Application.Features.Catalogs.Tags.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class TagsController : BaseApiController<TagsController>
    {
        [Authorize(Policy = Permissions.Tags.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTagsQuery()));
        }

        [Authorize(Policy = Permissions.Tags.View)]
        [HttpGet("catalog/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForPartner()
        {
            return Ok(await _mediator.Send(new GetAllTagsForPartnerQuery()));
        }

        [Authorize(Policy = Permissions.Tags.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetTagByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Tags.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(TagRequest request)
{
            return Ok(await _mediator.Send(new AddEditTagCommand() { TagRequest = request }));
        }

        [Authorize(Policy = Permissions.Tags.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTagCommand() { Id = id }));
        }
    }
}
