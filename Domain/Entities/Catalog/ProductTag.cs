using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class ProductTag : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }

        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
