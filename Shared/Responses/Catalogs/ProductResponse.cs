using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;

namespace SharedR.Responses.Catalogs
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
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
        public PartnerResponse Partner { get; set; } = new();
        public BrandResponse Brand { get; set; } = new();
        public List<ProductImageResponse> Images { get; set; } = new();
        public List<ProductCategoriesResponse> ProductCategories { get; set; } = new();
        public List<ProductTagsResponse> ProductTags { get; set; } = new();
        public List<ProductReviewResponse> ProductReviews { get; set; } = new();
    }
}
