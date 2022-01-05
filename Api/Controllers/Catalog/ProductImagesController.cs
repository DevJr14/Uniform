using Application.Features.Catalogs.ProductImages.Commands;
using Application.Features.Catalogs.ProductImages.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductImagesController : BaseApiController<ProductImagesController>
    {

        [Authorize(Policy = Permissions.ProductImages.View)]
        [HttpGet("catalog/[controller]/get-all-for-product")]
        public async Task<IActionResult> GetAllForProduct(Guid productId)
        {
            return Ok(await _mediator.Send(new GetProductImagesByProductIdQuery() { ProductId = productId }));
        }

        [Authorize(Policy = Permissions.ProductImages.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductImageByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.ProductImages.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductImageRequest request)
        {
            return Ok(await _mediator.Send(new AddEditProductImageCommand() { ProductImageRequest = request }));
        }

        [Authorize(Policy = Permissions.ProductImages.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductImageCommand() { Id = id }));
        }
    }
}
