using System;

namespace SharedR.Responses.Promotions
{
    public class DiscountResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Cost { get; set; }
        public Guid? DiscountId { get; set; }
    }
}
