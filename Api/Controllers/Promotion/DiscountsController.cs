using Application.Features.Promotions.Discounts.Commands;
using Application.Features.Promotions.Discounts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.Promotions;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Promotion
{
    public class DiscountsController : BaseApiController<DiscountsController>
    {
        [Authorize(Policy = Permissions.Discounts.View)]
        [HttpGet("promotion/[controller]/get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllDiscountsQuery()));
        }

        [Authorize(Policy = Permissions.Categories.View)]
        [HttpGet("promotion/[controller]/get-all-for-partner")]
        public async Task<IActionResult> GetAllForPartner()
        {
            return Ok(await _mediator.Send(new GetAllDiscountsForPartnerQuery()));
        }

        [Authorize(Policy = Permissions.Discounts.View)]
        [HttpGet("promotion/[controller]/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetDiscountByIdQuery() { Id = id }));
        }

        [Authorize(Policy = Permissions.Discounts.Create)]
        [HttpPost("promotion/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(DiscountRequest request)
        {
            return Ok(await _mediator.Send(new AddEditDiscountCommand() { DiscountRequest = request }));
        }

        [Authorize(Policy = Permissions.Discounts.Delete)]
        [HttpDelete("promotion/[controller]/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteDiscountCommand() { Id = id }));
        }
    }
}
