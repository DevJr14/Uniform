using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.ShoppingCart;
using SharedR.Requests.ShoppingCart;
using SharedR.Responses.ShoppingCart;
using SharedR.Wrapper;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.ShoppingCarts
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete()
        {
            var response = await _httpClient.DeleteAsync($"{ShoppingCartEndpoints.Delete}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<ShoppingCartResponse>> GetCart()
        {
            var response = await _httpClient.GetAsync(ShoppingCartEndpoints.GetCart);
            return await response.ToResult<ShoppingCartResponse>();
        }

        public async Task<IResult<Guid>> RemoveFromCart(Guid cartDetailsId)
        {
            var response = await _httpClient.PostAsJsonAsync(ShoppingCartEndpoints.RemoveFromCart, cartDetailsId);
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<Guid>> Save(ShoppingCartRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(ShoppingCartEndpoints.Save, request);
            return await response.ToResult<Guid>();
        }
    }
}
