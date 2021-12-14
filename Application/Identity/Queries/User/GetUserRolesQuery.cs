using Application.Identity.Interfaces;
using MediatR;
using Shared.Responses.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.User
{
    public class GetUserRolesQuery : IRequest<IResult<UserRolesResponse>>
    {
        public string UserId { get; set; }
    }

    internal class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IResult<UserRolesResponse>>
    {
        private readonly IUserService _userService;

        public GetUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult<UserRolesResponse>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetRolesAsync(request.UserId);
        }
    }
}
