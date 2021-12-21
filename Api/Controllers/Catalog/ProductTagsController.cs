using Application.Features.Catalogs.ProductTags.Commands;
using Application.Features.Catalogs.ProductTags.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Catalogs;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductTagsController : BaseApiController<ProductTagsController>
    {
        [Authorize(Policy = Permissions.ProductTags.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductTagsQuery()));
        }

        [Authorize(Policy = Permissions.ProductTags.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductTagsRequest request)
        {
            return Ok(await _mediator.Send(new AddEditProductTagsCommand() { ProductTagsRequest = request }));
        }
    }
}
