using Application.Features.Catalogs.ProductReviews.Commands;
using Application.Features.Catalogs.ProductReviews.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductReviewsController : BaseApiController<ProductReviewsController>
    {
        [Authorize(Policy = Permissions.ProductReviews.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetReviewsForAllProductsQuery()));
        }

        [Authorize(Policy = Permissions.ProductReviews.View)]
        [HttpGet("catalog/[controller]/get-all-for-product")]
        public async Task<IActionResult> GetAllForProduct(Guid productId)
        {
            return Ok(await _mediator.Send(new GetAllProductReviewsQuery() { ProductId = productId }));
        }

        [Authorize(Policy = Permissions.ProductReviews.View)]
        [HttpGet("catalog/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductReviewByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.ProductReviews.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductReviewRequest request)
        {
            return Ok(await _mediator.Send(new AddEditProductReviewCommand() { ProductReviewRequest = request }));
        }

        [Authorize(Policy = Permissions.ProductReviews.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductReviewCommand() { Id = id }));
        }
    }
}
