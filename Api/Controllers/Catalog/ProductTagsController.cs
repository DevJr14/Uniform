using Application.Features.Catalogs.ProductTags.Commands;
using Application.Features.Catalogs.ProductTags.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductTagsController : BaseApiController<ProductTagsController>
    {
        [Authorize(Policy = Permissions.ProductTags.View)]
        [HttpGet("catalog/[controller]/get-all-for-product")]
        public async Task<IActionResult> GetAll(Guid productId)
        {
            return Ok(await _mediator.Send(new GetAllProductTagsQuery() { ProductId = productId }));
        }

        [Authorize(Policy = Permissions.ProductTags.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductTagsRequest request)
        {
            return Ok(await _mediator.Send(new AddEditProductTagsCommand() { ProductTagsRequest = request }));
        }
    }
}
