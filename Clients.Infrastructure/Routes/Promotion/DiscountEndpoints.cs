using System;

namespace Clients.Infrastructure.Routes.Promotion
{
    public static class DiscountEndpoints
    {
        public static string GetAll = "api/v1/promotion/discounts/get-all";
        public static string Save = "api/v1/promotion/discounts/add-edit";
        public static string Delete = "api/v1/promotion/discounts/delete?id=";

        public static string GetForPartner()
        {
            return $"api/v1/promotion/discounts/get-all-for-partner";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/promotion/discounts/get-by-id?id={id}";
        }
    }
}
