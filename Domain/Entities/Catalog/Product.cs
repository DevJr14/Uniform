using Domain.Contracts;
using Domain.Entities.Partners;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Catalog
{
    public class Product : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public bool AllowReview { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public bool IsNew { get; set; }
        public Guid BrandId { get; set; }
        public Guid PartnerId { get; set; }

        public Partner Partner { get; set; }
        public Brand Brand { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }
        public List<ProductTags> ProductTags { get; set; }
        public List<ProductReviews> ProductReviews { get; set; }

    }
}
