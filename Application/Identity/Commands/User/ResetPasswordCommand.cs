using Application.Identity.Interfaces;
using MediatR;
using Shared.Requests.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class ResetPasswordCommand : IRequest<IResult>
    {
        public ResetPasswordRequest ResetPasswordRequest { get; set; }
    }

    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IResult>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            return await _userService.ResetPasswordAsync(command.ResetPasswordRequest);
        }
    }
}
