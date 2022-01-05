using Application.Features.Catalogs.Inventories.Commands;
using Application.Features.Catalogs.Inventories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Catalogs;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Catalog
{
    public class InventoriesController : BaseApiController<InventoriesController>
    {
        [Authorize(Policy = Permissions.Inventories.View)]
        [HttpGet("catalog/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllInventoriesQuery()));
        }

        [Authorize(Policy = Permissions.Inventories.View)]
        [HttpGet("catalog/[controller]/get-for-product")]
        public async Task<IActionResult> GetForProduct(Guid productId)
        {
            return Ok(await _mediator.Send(new GetInventoryByProductIdQuery() { ProductId = productId }));
        }

        [Authorize(Policy = Permissions.Inventories.Create)]
        [HttpPost("catalog/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(InventoryRequest request)
        {
            return Ok(await _mediator.Send(new AddEditInventoryCommand() { InventoryRequest = request }));
        }

        [Authorize(Policy = Permissions.Inventories.Delete)]
        [HttpDelete("catalog/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteInventoryCommand() { Id = id }));
        }
    }
}
