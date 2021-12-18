using SharedR.Responses.Identity;
using System.Collections.Generic;

namespace SharedR.Requests.Identity
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}
