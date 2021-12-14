using Application.Identity.Interfaces;
using MediatR;
using Shared.Responses.Identity;
using Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.User
{
    public class GetAllUsersQuery : IRequest<Result<List<UserResponse>>>
    {
    }

    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserResponse>>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllAsync();
        }
    }
}
