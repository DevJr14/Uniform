using Application.Identity.Interfaces;
using MediatR;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.User
{
    public class GetUsersCountQuery : IRequest<Result<int>>
    {
    }

    internal class GetUsersCountQueryHandler : IRequestHandler<GetUsersCountQuery, Result<int>>
    {
        private readonly IUserService _userService;

        public GetUsersCountQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<int>> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            var usersCount = await _userService.GetCountAsync();
            return await Result<int>.SuccessAsync(usersCount);
        }
    }
}
