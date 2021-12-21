using SharedR.Responses.Promotions;
using System;

namespace SharedR.Responses.Catalogs
{
    public class ProductPriceResponse
    {
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Cost { get; set; }
        public Guid ProdictId { get; set; }
        public Guid? DiscountId { get; set; }
        public DiscountResponse Discount { get; set; } = new();
    }
}
