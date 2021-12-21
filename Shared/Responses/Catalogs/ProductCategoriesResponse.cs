using System;

namespace SharedR.Responses.Catalogs
{
    public class ProductCategoriesResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public ProductResponse Product { get; set; } = new();
        public CategoryResponse Category { get; set; } = new();
    }
}
