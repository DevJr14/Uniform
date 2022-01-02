using Application.Identity.Interfaces;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class UpdateUserProfileCommand : IRequest<IResult>
    {
        public UpdateProfileRequest UpdateProfileRequest { get; set; }
        public string UserId { get; set; }
    }

    internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, IResult>
    {
        private readonly IAccountService _accountService;

        public UpdateUserProfileCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IResult> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
        {
            return await _accountService.UpdateProfileAsync(command.UpdateProfileRequest, command.UserId);
        }
    }
}
