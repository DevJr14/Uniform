using Application.Features.Catalogs.ProductPrices.Commands;
using Application.Features.Catalogs.ProductPrices.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductPriceController : BaseApiController<ProductPriceController>
    {
        [Authorize(Policy = Permissions.ProductPrices.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll(Guid productId)
        {
            return Ok(await _mediator.Send(new GetAllProductPriceQuery() {ProductId = productId }));
        }

        [Authorize(Policy = Permissions.ProductPrices.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductPriceByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.ProductPrices.View)]
        [HttpGet("catalog/[controller]/get-for-product")]
        public async Task<IActionResult> GetForProduct(Guid productId)
        {
            return Ok(await _mediator.Send(new GetProductPriceByProductIdQuery() { ProductId = productId }));
        }

        [Authorize(Policy = Permissions.ProductPrices.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductPriceRequest request)
{
            return Ok(await _mediator.Send(new AddEditProductPriceCommand() { ProductPriceRequest = request }));
        }

        [Authorize(Policy = Permissions.ProductPrices.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductPriceCommand() { Id = id }));
        }
    }
}
