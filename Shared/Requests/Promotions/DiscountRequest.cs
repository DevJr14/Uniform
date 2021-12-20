using System;

namespace SharedR.Requests.Promotions
{
    public class DiscountRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
