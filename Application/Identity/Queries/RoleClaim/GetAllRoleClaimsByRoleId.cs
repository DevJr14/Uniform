using Application.Identity.Interfaces;
using MediatR;
using SharedR.Responses.Identity;
using SharedR.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.RoleClaim
{
    public class GetAllRoleClaimsByRoleId : IRequest<Result<List<RoleClaimResponse>>>
    {
        public string RoleId { get; set; }
    }

    internal class GetAllRoleClaimsByRoleIdHandler : IRequestHandler<GetAllRoleClaimsByRoleId, Result<List<RoleClaimResponse>>>
    {
        private readonly IRoleClaimService _roleClaimService;

        public GetAllRoleClaimsByRoleIdHandler(IRoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        public async Task<Result<List<RoleClaimResponse>>> Handle(GetAllRoleClaimsByRoleId request, CancellationToken cancellationToken)
        {
            return await _roleClaimService.GetAllByRoleIdAsync(request.RoleId);
        }
    }
}
