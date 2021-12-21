using System;

namespace SharedR.Requests.Catalogs
{
    public class ProductRequest
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
    }
}
