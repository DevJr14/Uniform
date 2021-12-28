using System;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class CategoriesEndpoints
    {
        public static string GetAll = "api/v1/catalog/categories/get-all";
        public static string Save = "api/v1/catalog/categories/add-edit";
        public static string Delete = "api/v1/catalog/categories/delete?id=";

        public static string GetForUser(Guid userId)
        {
            return $"api/v1/catalog/categories/get-for-user?userid={userId}";
        }

        public static string GetForPartner()
        {
            return $"api/v1/catalog/categories/get-all-for-partner";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/catalog/categories/get-by-id?id={id}";
        }
    }
}
