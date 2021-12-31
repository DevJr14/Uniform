using System;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class InventoriesEndpoints
    {
        public static string GetAll = "api/v1/catalog/inventories/get-all";
        public static string Save = "api/v1/catalog/inventories/add-edit";
        public static string Delete = "api/v1/catalog/inventories/delete?id=";

        public static string GetForProduct(Guid productId)
        {
            return $"api/v1/catalog/inventories/get-for-product?productid={productId}";
        }
    }
}
