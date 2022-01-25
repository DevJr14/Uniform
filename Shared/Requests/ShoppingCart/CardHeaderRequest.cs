using System;

namespace SharedR.Requests.ShoppingCart
{
    public class CartHeaderRequest
    {
        public Guid CartHederId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
