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

namespace Clients.Infrastructure.Managers.Catalogs.Tags
{
    public class TagManager : ITagManager
    {
        private readonly HttpClient _httpClient;

        public TagManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid tagId)
        {
            var response = await _httpClient.DeleteAsync($"{TagsEndpoints.Delete}{tagId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<TagResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(TagsEndpoints.GetAll);
            return await response.ToResult<List<TagResponse>>();
        }

        public async Task<IResult<TagResponse>> GetById(Guid tagId)
        {
            var response = await _httpClient.GetAsync(TagsEndpoints.GetById(tagId));
            return await response.ToResult<TagResponse>();
        }

        public async Task<IResult<List<TagResponse>>> GetForPartner()
        {
            var response = await _httpClient.GetAsync(TagsEndpoints.GetForPartner());
            return await response.ToResult<List<TagResponse>>();
        }

        public async Task<IResult<List<TagResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(TagsEndpoints.GetForUser(userId));
            return await response.ToResult<List<TagResponse>>();
        }

        public async Task<IResult<Guid>> Save(TagRequest tagRequest)
{
            var response = await _httpClient.PostAsJsonAsync(TagsEndpoints.Save, tagRequest);
            return await response.ToResult<Guid>();
        }
    }
}
