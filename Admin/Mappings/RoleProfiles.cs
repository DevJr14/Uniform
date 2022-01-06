using AutoMapper;
using SharedR.Requests.Identity;
using SharedR.Responses.Identity;

namespace Admin.Mappings
{
    public class RoleProfiles : Profile
    {
        public RoleProfiles()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}
