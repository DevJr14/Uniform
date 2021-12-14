using Application.Identity.Interfaces;
using MediatR;
using Shared.Requests.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class UpdateUserStatusCommand : IRequest<IResult>
    {
        public ToggleUserStatusRequest UserStatusRequest { get; set; }
    }

    internal class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, IResult>
    {
        private readonly IUserService _userService;

        public UpdateUserStatusCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(UpdateUserStatusCommand command, CancellationToken cancellationToken)
        {
            return await _userService.ToggleUserStatusAsync(command.UserStatusRequest);
        }
    }
}
