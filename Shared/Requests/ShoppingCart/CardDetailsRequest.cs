using SharedR.Requests.Catalogs;
using System;

namespace SharedR.Requests.ShoppingCart
{
    public class CartDetailsRequest
    {
        public Guid CartDetailsId { get; set; }
        public Guid CartHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public ProductRequest Product { get; set; }
        public CartHeaderRequest CartHeader { get; set; }
    }
}
