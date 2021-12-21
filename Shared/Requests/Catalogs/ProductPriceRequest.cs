using System;

namespace SharedR.Requests.Catalogs
{
    public class ProductPriceRequest
    {
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Cost { get; set; }
        public Guid? DiscountId { get; set; }
    }
}
