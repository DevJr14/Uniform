using Application.Identity.Interfaces;
using MediatR;
using SharedR.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.User
{
    public class ExportUsersQuery : IRequest<Result<string>>
    {
    }

    internal class ExportUsersQueryHandler : IRequestHandler<ExportUsersQuery, Result<string>>
    {
        private readonly IUserService _userService;

        public ExportUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> Handle(ExportUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.ExportToExcelAsync();
        }
    }
}
