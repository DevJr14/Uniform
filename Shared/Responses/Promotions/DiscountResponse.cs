using System;

namespace SharedR.Responses.Promotions
{
    public class DiscountResponse
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
