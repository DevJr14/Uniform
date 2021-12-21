using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class ProductCategory : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
