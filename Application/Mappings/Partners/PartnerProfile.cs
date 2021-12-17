using AutoMapper;
using Domain.Entities.Partners;
using Shared.Requests.Partners;
using Shared.Responses.Partners;

namespace Application.Mappings.Partners
{
    public class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<PartnerRequest, Partner>();
            CreateMap<Partner, PartnerResponse>();
        }
    }
}
