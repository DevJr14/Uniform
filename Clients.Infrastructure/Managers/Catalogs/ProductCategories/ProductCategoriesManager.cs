using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Catalog;
using Shared.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.ProductCategories
{
    public class ProductCategoriesManager : IProductCategoriesManager
    {
        private readonly HttpClient _httpClient;

        public ProductCategoriesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<ProductCategoriesResponse>>> GetForProduct(Guid productId)
        {
            var response = await _httpClient.GetAsync(ProductCategoriesEndpoints.GetForProduct(productId));
            return await response.ToResult<List<ProductCategoriesResponse>>();
        }

        public async Task<IResult<Guid>> Save(ProductCategoriesRequest productCategoriesRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(ProductCategoriesEndpoints.Save, productCategoriesRequest);
            return await response.ToResult<Guid>();
        }
    }
}
