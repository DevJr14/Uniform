using Application.Identity.Interfaces;
using MediatR;
using SharedR.Wrapper;
using SharedR.Requests.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class UpdateUserProfileCommand : IRequest<IResult>
    {
        public UpdateProfileRequest UpdateProfileRequest { get; set; }
    }

    internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, IResult>
    {
        private readonly IAccountService _accountService;
        private readonly ICurrentUserService _currentUser;

        public UpdateUserProfileCommandHandler(IAccountService accountService, ICurrentUserService currentUser)
        {
            _accountService = accountService;
            _currentUser = currentUser;
        }

        public async Task<IResult> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
        {
            return await _accountService.UpdateProfileAsync(command.UpdateProfileRequest, _currentUser.UserId);
        }
    }
}
