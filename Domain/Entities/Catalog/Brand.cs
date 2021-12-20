using Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Catalog
{
    public class Brand : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
