using Application.Identity.Queries.RoleClaim;
using Microsoft.AspNetCore.Mvc;
using SharedR.Requests.Identity;
using System.Threading.Tasks;

namespace Api.Controllers.Identity
{
    public class TokenController : BaseApiController<TokenController>
    {
        /// <summary>
        /// Get Token (Email, Password)
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("auth/[controller]/get-token")]
        public async Task<ActionResult> Get(TokenRequest request)
        {
            var response = await _mediator.Send(new GetTokenQuery() { TokenRequest = request });
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("auth/[controller]/get-refresh-token")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await _mediator.Send(new GetRefreshTokenQuery() { RefreshTokenRequest = request });
            return Ok(response);
        }
    }
}
