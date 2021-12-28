using System;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class TagsEndpoints
    {
        public static string GetAll = "api/v1/catalog/tags/get-all";
        public static string Save = "api/v1/catalog/tags/add-edit";
        public static string Delete = "api/v1/catalog/tags/delete?id=";

        public static string GetForUser(Guid userId)
        {
            return $"api/v1/catalog/tags/get-for-user?userid={userId}";
        }

        public static string GetForPartner()
        {
            return $"api/v1/catalog/tags/get-all-for-partner";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/catalog/tags/get-by-id?id={id}";
        }
    }
}
