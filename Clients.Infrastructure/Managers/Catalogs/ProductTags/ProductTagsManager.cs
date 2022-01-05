using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Catalog;
using SharedR.Wrapper;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Catalogs.ProductTags
{
    public class ProductTagsManager : IProductTagsManager
    {
        private readonly HttpClient _httpClient;

        public ProductTagsManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<ProductTagsResponse>>> GetForProduct(Guid productId)
        {
            var response = await _httpClient.GetAsync(ProductTagsEndpoints.GetForProduct(productId));
            return await response.ToResult<List<ProductTagsResponse>>();
        }

        public async Task<IResult<Guid>> Save(ProductTagsRequest productTagsRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(ProductTagsEndpoints.Save, productTagsRequest);
            return await response.ToResult<Guid>();
        }
    }
}
