using Application.Identity.Interfaces;
using MediatR;
using SharedR.Requests.Identity;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class ForgotPasswordCommand : IRequest<IResult>
    {
        public ForgotPasswordRequest ForgotPasswordRequest { get; set; }
        public string Origin { get; set; }
    }

    internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult>
    {
        private readonly IUserService _userService;

        public ForgotPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            return await _userService.ForgotPasswordAsync(command.ForgotPasswordRequest, command.Origin);
        }
    }
}
