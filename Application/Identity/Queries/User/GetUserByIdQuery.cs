using Application.Identity.Interfaces;
using MediatR;
using Shared.Responses.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.User
{
    public class GetUserByIdQuery : IRequest<IResult<UserResponse>>
    {
        public string UserId { get; set; }
    }

    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IResult<UserResponse>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAsync(request.UserId);
        }
    }
}
