using SharedR.Requests.ShoppingCart;
using SharedR.Responses.ShoppingCart;
using SharedR.Wrapper;
using System;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.ShoppingCarts
{
    public interface IShoppingCartManager : IManager
    {
        Task<IResult<ShoppingCartResponse>> GetCart();
        Task<IResult<Guid>> Save(ShoppingCartRequest request);
        Task<IResult<Guid>> Delete();
        Task<IResult<Guid>> RemoveFromCart(Guid cartDetailsId);
    }
}
