using AutoMapper;
using Infrastructure.Models.Identity;
using Shared.Requests.Identity;
using Shared.Responses.Identity;

namespace Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, ApplicationRoleClaim>()
                .ForMember(nameof(ApplicationRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(ApplicationRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, ApplicationRoleClaim>()
                .ForMember(nameof(ApplicationRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(ApplicationRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}
