using Domain.Contracts;
using System;

namespace Domain.Entities.Catalog
{
    public class ProductReview : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public string ReplyText { get; set; }

        public Product Product { get; set; }
    }
}
