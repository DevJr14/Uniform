using Application.Identity.Interfaces;
using MediatR;
using SharedR.Requests.Identity;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class UpdateUserRolesCommand : IRequest<IResult>
    {
        public UpdateUserRolesRequest UpdateUserRolesRequest { get; set; }
    }

    internal class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, IResult>
    {
        private readonly IUserService _userService;

        public UpdateUserRolesCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(UpdateUserRolesCommand command, CancellationToken cancellationToken)
        {
            return await _userService.UpdateRolesAsync(command.UpdateUserRolesRequest);
        }
    }
}
