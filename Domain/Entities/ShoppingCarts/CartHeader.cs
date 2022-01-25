using Domain.Contracts;
using System;

namespace Domain.Entities.ShoppingCarts
{
    public class CartHeader : AuditableEntity<Guid>
    {
        public string CouponCode { get; set; }
    }
}
