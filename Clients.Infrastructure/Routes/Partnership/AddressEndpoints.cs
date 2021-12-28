using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Routes.Partnership
{
    public static class AddressEndpoints
    {
        public static string GetAll = "api/v1/partnership/addresses/get-all";
        public static string Save = "api/v1/partnership/addresses/add-edit";
        public static string Delete = "api/v1/partnership/addresses/delete?id=";

        public static string GetForUser(Guid userId)
        {
            return $"api/v1/partnership/addresses/get-for-user?userid={userId}";
        }

        public static string GetForPartner(Guid partnerId)
        {
            return $"api/v1/partnership/addresses/get-all-for-partner?partnerid={partnerId}";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/partnership/addresses/get-by-id?id={id}";
        }
    }
}
