using Application.Identity.Interfaces;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class ChangePasswordCommand : IRequest<IResult>
    {
        public ChangePasswordRequest ChangePasswordRequest { get; set; }
    }

    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, IResult>
    {
        private readonly IAccountService _accountService;
        private readonly ICurrentUserService _currentUser;

        public ChangePasswordCommandHandler(IAccountService accountService, ICurrentUserService currentUser)
        {
            _accountService = accountService;
            _currentUser = currentUser;
        }

        public async Task<IResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            return await _accountService.ChangePasswordAsync(command.ChangePasswordRequest, _currentUser.UserId);
        }
    }
}
