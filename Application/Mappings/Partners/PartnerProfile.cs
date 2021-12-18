using AutoMapper;
using Domain.Entities.Partners;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;

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
