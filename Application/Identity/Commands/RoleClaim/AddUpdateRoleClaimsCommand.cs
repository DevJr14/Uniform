using Application.Identity.Interfaces;
using MediatR;
using Shared.Requests.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.RoleClaim
{
    public class AddUpdateRoleClaimsCommand : IRequest<Result<string>>
    {
        public RoleClaimRequest RoleClaimRequest { get; set; }
    }

    internal class AddUpdateRoleClaimsCommandHandler : IRequestHandler<AddUpdateRoleClaimsCommand, Result<string>>
    {
        private readonly IRoleClaimService _roleClaimService;

        public AddUpdateRoleClaimsCommandHandler(IRoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        public async Task<Result<string>> Handle(AddUpdateRoleClaimsCommand command, CancellationToken cancellationToken)
        {
            return await _roleClaimService.SaveAsync(command.RoleClaimRequest);
        }
    }
}
