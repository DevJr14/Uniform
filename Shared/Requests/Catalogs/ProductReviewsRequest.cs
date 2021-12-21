using System;

namespace SharedR.Requests.Catalogs
{
    public class ProductReviewsRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public string ReplyText { get; set; }
    }
}
