using Application.Identity.Interfaces;
using MediatR;
using SharedR.Responses.Identity;
using Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.RoleClaim
{
    public class GetAllRoleClaimsQuery : IRequest<Result<List<RoleClaimResponse>>>
    {
    }

    internal class GetAllRoleClaimsQueryHandler : IRequestHandler<GetAllRoleClaimsQuery, Result<List<RoleClaimResponse>>>
    {
        private readonly IRoleClaimService _roleClaimService;

        public GetAllRoleClaimsQueryHandler(IRoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        public async Task<Result<List<RoleClaimResponse>>> Handle(GetAllRoleClaimsQuery request, CancellationToken cancellationToken)
        {
            return await _roleClaimService.GetAllAsync();
        }
    }
}
