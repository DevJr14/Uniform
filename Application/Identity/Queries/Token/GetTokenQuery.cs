using Application.Identity.Interfaces;
using MediatR;
using Shared.Requests.Identity;
using Shared.Responses.Identity;
using Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries.RoleClaim
{
    public class GetTokenQuery : IRequest<Result<TokenResponse>>
    {
        public TokenRequest TokenRequest { get; set; }
    }

    internal class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, Result<TokenResponse>>
    {
        private readonly ITokenService _tokenService;

        public GetTokenQueryHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<Result<TokenResponse>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.LoginAsync(request.TokenRequest);
            if (token.Data != null)
            {
                return await Result<TokenResponse>.SuccessAsync(token.Data, "Token Successfully Generated");
            }
            return await Result<TokenResponse>.FailAsync(token.Messages);
        }
    }
}
