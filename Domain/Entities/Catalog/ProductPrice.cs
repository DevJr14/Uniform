using Domain.Contracts;
using Domain.Entities.Promotions;
using System;

namespace Domain.Entities.Catalog
{
    public class ProductPrice : AuditableEntity<Guid>
    {
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Cost { get; set; }
        public Guid? DiscountId { get; set; }

        public Discount Discount { get; set; }
    }
}
