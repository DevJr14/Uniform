using System;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class ProductImagesEndpoints
    {
        public static string Save = "api/v1/catalog/productImages/add-edit";
        public static string Delete = "api/v1/catalog/productImages/delete?id=";

        public static string GetForProduct(Guid productId)
        {
            return $"api/v1/catalog/productImages/get-all-for-product?productId={productId}";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/catalog/productImages/get-by-id?id={id}";
        }
    }
}
