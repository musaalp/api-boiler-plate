using Api.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dtos;
using Sdk.Api.Utils;
using Service.Auth.Login;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponseDto<LoginServiceResponse>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponseDto), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
            var loginResponse = await _mediator.Send(new LoginServiceRequest
            {
                UserName = model.Username,
                Password = model.Password,
            }, cancellationToken);

            return ApiActionResult.Ok(loginResponse);
        }
    }
}
