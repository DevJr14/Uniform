using Application.Identity.Interfaces;
using MediatR;
using Shared.Requests.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class RegisterUserCommand : IRequest<IResult>
    {
        public RegisterRequest RegisterRequest { get; set; }
        public string Origin { get; set; }
    }

    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.RegisterAsync(command.RegisterRequest, command.Origin);
        }
    }
}
