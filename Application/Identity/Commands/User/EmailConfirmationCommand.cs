using Application.Identity.Interfaces;
using MediatR;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class EmailConfirmationCommand : IRequest<IResult>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }

    internal class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, IResult>
    {
        private readonly IUserService _userService;

        public EmailConfirmationCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> Handle(EmailConfirmationCommand command, CancellationToken cancellationToken)
        {
            return await _userService.ConfirmEmailAsync(command.UserId, command.Code);
        }
    }
}
