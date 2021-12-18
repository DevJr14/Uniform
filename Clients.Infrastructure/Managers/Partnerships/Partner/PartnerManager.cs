using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Partnership;
using SharedR.Requests.Partners;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SharedR.Responses.Partners;

namespace Clients.Infrastructure.Managers.Partnerships.Partner
{
    public class PartnerManager : IPartnerManger
    {
        private readonly HttpClient _httpClient;

        public PartnerManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid partnerId)
        {
            var response = await _httpClient.DeleteAsync($"{PartnerEndpoints.Delete}/{partnerId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<PartnerResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(PartnerEndpoints.GetAll);
            return await response.ToResult<List<PartnerResponse>>();
        }

        public async Task<IResult<PartnerResponse>> GetById(Guid partnerId)
        {
            var response = await _httpClient.GetAsync(PartnerEndpoints.GetById(partnerId));
            return await response.ToResult<PartnerResponse>();
        }

        public async Task<IResult<List<PartnerResponse>>> GetByVerification(bool isVerified)
        {
            var response = await _httpClient.GetAsync(PartnerEndpoints.GetByVerificationStatus(isVerified));
            return await response.ToResult<List<PartnerResponse>>();
        }

        public async Task<IResult<List<PartnerResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(PartnerEndpoints.GetForUser(userId));
            return await response.ToResult<List<PartnerResponse>>();
        }

        public async Task<IResult<Guid>> Save(PartnerRequest partnerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(PartnerEndpoints.Save, partnerRequest);
            return await response.ToResult<Guid>();
        }
    }
}
