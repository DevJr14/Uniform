using System;

namespace SharedR.Responses.Catalogs
{
    public class ProductTagsResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }
        public ProductResponse Product { get; set; } = new();
        public TagResponse Tag { get; set; } = new();
    }
}
