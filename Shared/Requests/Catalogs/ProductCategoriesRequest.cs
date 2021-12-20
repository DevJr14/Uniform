using System;

namespace SharedR.Requests.Catalogs
{
    public class ProductCategoriesRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
