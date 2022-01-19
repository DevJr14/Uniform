using System;

namespace SharedR.Requests.Promotions
{
    public class DiscountRequest
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(10);
    }
}
