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

namespace Clients.Infrastructure.Managers.Catalogs.Categories
{
    public class CategoryManager : ICategoryManager
    {
        private readonly HttpClient _httpClient;

        public CategoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid categoryId)
        {
            var response = await _httpClient.DeleteAsync($"{CategoriesEndpoints.Delete}{categoryId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<CategoryResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(CategoriesEndpoints.GetAll);
            return await response.ToResult<List<CategoryResponse>>();
        }

        public async Task<IResult<CategoryResponse>> GetById(Guid categoryId)
        {
            var response = await _httpClient.GetAsync(CategoriesEndpoints.GetById(categoryId));
            return await response.ToResult<CategoryResponse>();
        }

        public async Task<IResult<List<CategoryResponse>>> GetForPartner()
        {
            var response = await _httpClient.GetAsync(CategoriesEndpoints.GetForPartner());
            return await response.ToResult<List<CategoryResponse>>();
        }

        public async Task<IResult<List<CategoryResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(CategoriesEndpoints.GetForUser(userId));
            return await response.ToResult<List<CategoryResponse>>();
        }

        public async Task<IResult<Guid>> Save(CategoryRequest categoryRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(CategoriesEndpoints.Save, categoryRequest);
            return await response.ToResult<Guid>();
        }
    }
}
