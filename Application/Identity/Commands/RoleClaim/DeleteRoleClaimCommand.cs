using Application.Identity.Interfaces;
using MediatR;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.RoleClaim
{
    public class DeleteRoleClaimCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
    }

    internal class DeleteRoleClaimCommandHandler : IRequestHandler<DeleteRoleClaimCommand, Result<string>>
    {
        private readonly IRoleClaimService _roleClaimService;

        public DeleteRoleClaimCommandHandler(IRoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        public async Task<Result<string>> Handle(DeleteRoleClaimCommand command, CancellationToken cancellationToken)
        {
            return await _roleClaimService.DeleteAsync(command.Id);
        }
    }
}
