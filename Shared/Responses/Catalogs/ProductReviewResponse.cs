using System;

namespace SharedR.Responses.Catalogs
{
    public class ProductReviewResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public string ReplyText { get; set; }
        public ProductResponse Product { get; set; } = new();
    }
}
