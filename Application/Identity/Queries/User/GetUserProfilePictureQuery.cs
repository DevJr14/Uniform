using Application.Identity.Interfaces;
using MediatR;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.User
{
    public class GetUserProfilePictureQuery : IRequest<IResult<string>>
    {
        public string UserId { get; set; }
    }

    internal class GetUserProfilePictureQueryHandler : IRequestHandler<GetUserProfilePictureQuery, IResult<string>>
    {
        private readonly IAccountService _accountService;

        public GetUserProfilePictureQueryHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IResult<string>> Handle(GetUserProfilePictureQuery query, CancellationToken cancellationToken)
        {
            return await _accountService.GetProfilePictureAsync(query.UserId);
        }
    }
}
