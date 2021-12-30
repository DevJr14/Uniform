using System;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class ProductPriceEndpoints
    {
        public static string GetAll = "api/v1/catalog/productprice/get-all";
        public static string Save = "api/v1/catalog/productprice/add-edit";
        public static string Delete = "api/v1/catalog/productprice/delete?id=";

        public static string GetForProduct(Guid productId)
        {
            return $"api/v1/catalog/productprice/get-for-product?productid={productId}";
        }

        public static string GetById(Guid id)
        {
            return $"api/v1/catalog/productprice/get-by-id?id={id}";
        }
    }
}
