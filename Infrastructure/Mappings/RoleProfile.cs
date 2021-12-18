using AutoMapper;
using Infrastructure.Models.Identity;
using SharedR.Responses.Identity;

namespace Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, ApplicationRole>().ReverseMap();
        }
    }
}
