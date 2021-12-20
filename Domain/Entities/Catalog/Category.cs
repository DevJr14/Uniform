using Domain.Contracts;
using Domain.Entities.Partners;
using System;

namespace Domain.Entities.Catalog
{
    public class Category : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid PartnerId { get; set; }

        public Partner Partner { get; set; }
    }
}
