using System.Collections.Generic;

namespace SharedR.Requests.ShoppingCart
{
    public class ShoppingCartRequest
    {
        public CartHeaderRequest CartHeader { get; set; }
        public List<CartDetailsRequest> CartDetails { get; set; }
    }
}
