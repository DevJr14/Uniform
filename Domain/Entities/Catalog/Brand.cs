using Domain.Contracts;
using Domain.Entities.Partners;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Catalog
{
    public class Brand : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PartnerId { get; set; }

        public Partner Partner { get; set; }
        public List<Product> Products { get; set; }
    }
}
