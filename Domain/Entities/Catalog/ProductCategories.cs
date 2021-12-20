using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class ProductCategories : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
