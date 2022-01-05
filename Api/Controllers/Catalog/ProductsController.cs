using Application.Features.Catalogs.Products.Commands;
using Application.Features.Catalogs.Products.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductsController : BaseApiController<ProductsController>
    {
        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet("catalog/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForProduct()
        {
            return Ok(await _mediator.Send(new GetAllProductsForPartnerQuery()));
        }

        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Products.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductRequest request)
        {
            return Ok(await _mediator.Send(new AddEditProductCommand() { ProductRequest = request }));
        }

        [Authorize(Policy = Permissions.Products.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand() { Id = id }));
        }
    }
}
