using Application.Identity.Interfaces;
using MediatR;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.Role
{
    public class DeleteRoleCommand : IRequest<Result<string>>
    {
        public string RoleId { get; set; }
    }

    internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<string>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            return await _roleService.DeleteAsync(command.RoleId);
        }
    }
}
