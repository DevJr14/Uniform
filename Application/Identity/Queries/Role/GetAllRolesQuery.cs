using Application.Identity.Interfaces;
using MediatR;
using SharedR.Responses.Identity;
using Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.Role
{
    public class GetAllRolesQuery : IRequest<Result<List<RoleResponse>>>
    {
    }

    internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetAllAsync();
        }
    }
}
