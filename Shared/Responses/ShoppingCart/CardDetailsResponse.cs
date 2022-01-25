using SharedR.Responses.Catalogs;
using System;

namespace SharedR.Responses.ShoppingCart
{
    public class CartDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid CartHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public ProductResponse Product { get; set; }
    }
}
