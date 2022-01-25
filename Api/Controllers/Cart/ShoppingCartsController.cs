using Application.Features.ShoppingCarts.Commands;
using Application.Features.ShoppingCarts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedR.Constants.Permission;
using SharedR.Requests.ShoppingCart;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Cart
{
    public class ShoppingCartsController : BaseApiController<ShoppingCartsController>
    {
        [Authorize(Policy = Permissions.ShoppingCarts.Create)]
        [HttpPost("shopping/[controller]/add-edit")]
        public async Task<IActionResult> AddOrEdit(ShoppingCartRequest request)
        {
            return Ok(await _mediator.Send(new AddEditCartCommand() { CartRequest = request }));
        }

        [Authorize(Policy = Permissions.ShoppingCarts.View)]
        [HttpGet("shopping/[controller]/get-shopping-cart")]
        public async Task<IActionResult> GetShoppingCart()
        {
            return Ok(await _mediator.Send(new GetCartForUserQuery()));
        }

        [Authorize(Policy = Permissions.ShoppingCarts.Delete)]
        [HttpDelete("shopping/[controller]/delete")]
        public async Task<IActionResult> Delete()
        {
            return Ok(await _mediator.Send(new DeleteCartCommand()));
        }

        [Authorize(Policy = Permissions.ShoppingCarts.Edit)]
        [HttpPost("shopping/[controller]/remove-from-cart")]
        public async Task<IActionResult> RemoveFromCart([FromQuery] string cartdetailsId)
        {
            return Ok(await _mediator.Send(new RemoveProductFromCartCommand() { CardDetailsId = new Guid(cartdetailsId) }));
        }
    }
}
