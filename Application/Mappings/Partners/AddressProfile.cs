using AutoMapper;
using Domain.Entities.Partners;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;

namespace Application.Mappings.Partners
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressRequest, Address>();
            CreateMap<Address, AddressResponse>();
        }
    }
}
