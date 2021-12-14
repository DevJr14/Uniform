using AutoMapper;
using Infrastructure.Models.Identity;
using Shared.Responses.Identity;

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
