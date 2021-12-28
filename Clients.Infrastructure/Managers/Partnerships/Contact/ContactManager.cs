using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Routes.Partnership;
using Shared.Wrapper;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Partnerships.Contact
{
    public class ContactManager : IContactManager
    {
        private readonly HttpClient _httpClient;

        public ContactManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> Delete(Guid contactId)
        {
            var response = await _httpClient.DeleteAsync($"{ContactEndpoints.Delete}{contactId}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<ContactResponse>>> GetAll()
        {
            var response = await _httpClient.GetAsync(ContactEndpoints.GetAll);
            return await response.ToResult<List<ContactResponse>>();
        }

        public async Task<IResult<ContactResponse>> GetById(Guid contactId)
        {
            var response = await _httpClient.GetAsync(ContactEndpoints.GetById(contactId));
            return await response.ToResult<ContactResponse>();
        }

        public async Task<IResult<List<ContactResponse>>> GetForPartner(Guid partnerId)
        {
            var response = await _httpClient.GetAsync(ContactEndpoints.GetForPartner(partnerId));
            return await response.ToResult<List<ContactResponse>>();
        }

        public async Task<IResult<List<ContactResponse>>> GetForUser(Guid userId)
        {
            var response = await _httpClient.GetAsync(ContactEndpoints.GetForUser(userId));
            return await response.ToResult<List<ContactResponse>>();
        }

        public async Task<IResult<Guid>> Save(ContactRequest contactRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(ContactEndpoints.Save, contactRequest);
            return await response.ToResult<Guid>();
        }
    }
}
