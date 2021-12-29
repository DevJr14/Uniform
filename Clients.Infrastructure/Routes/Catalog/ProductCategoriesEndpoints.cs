using System;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class ProductCategoriesEndpoints
    {
        public static string Save = "api/v1/catalog/productcategories/add-edit";

        public static string GetForProduct(Guid productId)
        {
            return $"api/v1/catalog/productcategories/get-all-for-product?productid={productId}";
        }
    }
}
