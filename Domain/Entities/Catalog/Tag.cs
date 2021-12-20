using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class Tag : AuditableEntity<Guid>
    {
        public string Name { get; set; }
    }
}
