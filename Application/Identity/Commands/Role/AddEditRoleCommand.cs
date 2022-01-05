using Application.Identity.Interfaces;
using MediatR;
using SharedR.Requests.Identity;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.Role
{
    public class AddEditRoleCommand : IRequest<Result<string>>
    {
        public RoleRequest RoleRequest { get; set; }
    }

    internal class AddEditRoleCommandHandler : IRequestHandler<AddEditRoleCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public AddEditRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<string>> Handle(AddEditRoleCommand command, CancellationToken cancellationToken)
        {
            return await _roleService.SaveAsync(command.RoleRequest);
        }
    }
}
