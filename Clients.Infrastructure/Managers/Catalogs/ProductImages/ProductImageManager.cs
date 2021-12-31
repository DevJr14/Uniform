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

namespace Clients.Infrastructure.Managers.Catalogs.ProductImages
{
    public class ProductImageManager : IProductImageManager
    {
        private readonly HttpClient _httpClient;

        public ProductImageManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid productImageId)
        {
            var response = await _httpClient.DeleteAsync($"{ProductImagesEndpoints.Delete}{productImageId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<ProductImageResponse>> GetById(Guid productImageId)
        {
            var response = await _httpClient.GetAsync(ProductImagesEndpoints.GetById(productImageId));
            return await response.ToResult<ProductImageResponse>();
        }

        public async Task<IResult<List<ProductImageResponse>>> GetForProduct(Guid productId)
        {
            var response = await _httpClient.GetAsync(ProductImagesEndpoints.GetForProduct(productId));
            return await response.ToResult<List<ProductImageResponse>>();
        }

        public async Task<IResult<Guid>> Save(ProductImageRequest productImageRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(ProductImagesEndpoints.Save, productImageRequest);
            return await response.ToResult<Guid>();
        }
    }
}
