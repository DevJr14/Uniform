using Domain.Contracts;
using Domain.Entities.Catalog;
using System;

namespace Domain.Entities.ShoppingCarts
{
    public class CartDetails : AuditableEntity<Guid>
    {
        public Guid CartHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public CartHeader CartHeader { get; set; }
        public Product Product { get; set; }
    }
}
