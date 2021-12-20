using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class Category : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
