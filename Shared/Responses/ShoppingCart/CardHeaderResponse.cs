using System;

namespace SharedR.Responses.ShoppingCart
{
    public class CartHeaderResponse
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string CouponCode { get; set; }
    }
}
