using Application.Features.Catalogs.ProductCategories.Commands;
using Application.Features.Catalogs.ProductCategories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Permission;
using SharedR.Requests.Catalogs;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class ProductCategoriesController : BaseApiController<ProductCategoriesController>
    {
        [Authorize(Policy = Permissions.ProductCategories.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductCategoriesQuery()));
        }

        [Authorize(Policy = Permissions.ProductCategories.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ProductCategoriesRequest request)
        {
            return Ok(await _mediator.Send(new AddEditProductCategoriesCommand() { ProductCategoriesRequest = request }));
        }
    }
}
