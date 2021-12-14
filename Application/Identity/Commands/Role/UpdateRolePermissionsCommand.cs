using Application.Identity.Interfaces;
using MediatR;
using Shared.Requests.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.Role
{
    public class UpdateRolePermissionsCommand : IRequest<Result<string>>
    {
        public PermissionRequest PermissionRequest { get; set; }
    }

    internal class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand, Result<string>>
    {
        private readonly IRoleService _roleService;

        public UpdateRolePermissionsCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<string>> Handle(UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
        {
            return await _roleService.UpdatePermissionsAsync(command.PermissionRequest);
        }
    }
}
