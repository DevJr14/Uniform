using Application.Identity.Interfaces;
using MediatR;
using Shared.Wrapper;
using SharedR.Requests.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands.User
{
    public class UpdateUserProfilePictureCommand : IRequest<IResult>
    {
        public UpdateProfilePictureRequest UpdateProfilePictureRequest { get; set; }
        public string UserId { get; set; }
    }

    internal class UpdateUserProfilePictureCommandHandler : IRequestHandler<UpdateUserProfilePictureCommand, IResult>
    {
        private readonly IAccountService _accountService;

        public UpdateUserProfilePictureCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IResult> Handle(UpdateUserProfilePictureCommand command, CancellationToken cancellationToken)
        {
            return await _accountService.UpdateProfilePictureAsync(command.UpdateProfilePictureRequest, command.UserId);
        }
    }
}
