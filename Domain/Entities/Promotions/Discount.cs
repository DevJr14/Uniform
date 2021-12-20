using Domain.Contracts;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Promotions
{
    public class Discount : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Product> Products { get; set; }
    }
}
