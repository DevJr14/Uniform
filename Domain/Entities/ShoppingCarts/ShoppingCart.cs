using System.Collections.Generic;

namespace Domain.Entities.ShoppingCarts
{
    public class ShoppingCart
    {
        public CartHeader CartHeader { get; set; }
        public List<CartDetails> CartDetails { get; set; }
    }
}
