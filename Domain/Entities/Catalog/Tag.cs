using Domain.Contracts;
using Domain.Entities.Partners;
using System;

namespace Domain.Entities.Catalog
{
    public class Tag : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public Guid PartnerId { get; set; }

        public Partner Partner { get; set; }
    }
}
