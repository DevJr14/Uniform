using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class BrandsEndpoints
    {
        public static string GetAll = "api/v1/catalog/brands/get-all";
        public static string Save = "api/v1/catalog/brands/add-edit";
        public static string Delete = "api/v1/catalog/brands/delete?id=";

        public static string GetForUser(Guid userId)
        {
            return $"api/v1/catalog/brands/get-for-user?userid={userId}";
        }

        public static string GetForPartner()
        {
            return $"api/v1/catalog/brands/get-all-for-partner";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/catalog/brands/get-by-id?id={id}";
        }
    }
}
