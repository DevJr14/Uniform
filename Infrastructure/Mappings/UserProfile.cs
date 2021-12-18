using AutoMapper;
using Infrastructure.Models.Identity;
using SharedR.Responses.Identity;

namespace Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserResponse>();
        }
    }
}
