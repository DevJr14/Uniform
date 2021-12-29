using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Routes.Catalog
{
    public static class ProductTagsEndpoints
    {
        public static string Save = "api/v1/catalog/producttags/add-edit";

        public static string GetForProduct(Guid productId)
        {
            return $"api/v1/catalog/producttags/get-all-for-product?productid={productId}";
        }
    }
}
