using Domain.Contracts;
using Domain.Entities.Catalog;
using Domain.Entities.Partners;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Promotions
{
    public class Discount : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PartnerId { get; set; }

        public Partner Partner { get; set; }
        public List<ProductPrice> Prices { get; set; }
    }
}
