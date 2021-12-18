using Application.Identity.Interfaces;
using MediatR;
using SharedR.Responses.Identity;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.Role
{
    public class GetAllPermissionsByRoleIdQuery : IRequest<Result<PermissionResponse>>
    {
        public string RoleId { get; set; }
    }

    internal class GetAllPermissionsByRoleIdQueryHandler : IRequestHandler<GetAllPermissionsByRoleIdQuery, Result<PermissionResponse>>
    {
        private readonly IRoleService _roleService;

        public GetAllPermissionsByRoleIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<PermissionResponse>> Handle(GetAllPermissionsByRoleIdQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetAllPermissionsAsync(request.RoleId);
        }
    }
}
