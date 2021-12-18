using Application.Identity.Interfaces;
using MediatR;
using SharedR.Requests.Identity;
using SharedR.Responses.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.RoleClaim
{
    public class GetRefreshTokenQuery : IRequest<Result<TokenResponse>>
    {
        public RefreshTokenRequest RefreshTokenRequest { get; set; }
    }

    internal class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, Result<TokenResponse>>
    {
        private readonly ITokenService _tokenService;

        public GetRefreshTokenQueryHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<Result<TokenResponse>> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetRefreshTokenAsync(request.RefreshTokenRequest);
            if (token != null)
            {
                return await Result<TokenResponse>.SuccessAsync(token.Data, "Refresh Token Successfully Generated");
            }
            return await Result<TokenResponse>.FailAsync("Failed To Generate Refresh Token");
        }
    }
}
