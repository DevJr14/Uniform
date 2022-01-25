using System.Collections.Generic;

namespace SharedR.Responses.ShoppingCart
{
    public class ShoppingCartResponse
    {
        public CartHeaderResponse CartHeader { get; set; }
        public List<CartDetailsResponse> CartDetails { get; set; }
    }
}
