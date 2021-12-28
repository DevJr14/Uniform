using System;

namespace Clients.Infrastructure.Routes.Partnership
{
    public static class BankAccountEndpoints
    {
        public static string GetAll = "api/v1/partnership/bankaccounts/get-all";
        public static string Save = "api/v1/partnership/bankaccounts/add-edit";
        public static string Delete = "api/v1/partnership/bankaccounts/delete?id=";

        public static string GetForUser(Guid userId)
        {
            return $"api/v1/partnership/bankaccounts/get-for-user?userid={userId}";
        }

        public static string GetForPartner(Guid partnerId)
        {
            return $"api/v1/partnership/bankaccounts/get-all-for-partner?partnerid={partnerId}";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/partnership/bankaccounts/get-by-id?id={id}";
        }
    }
}
