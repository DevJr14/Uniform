using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Api.Controllers
{
    /// <summary>
    /// Abstract BaseApi Controller Class
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    public class BaseApiController<T> : ControllerBase
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
