using CharityHub.Presentation.Bases;
using Microsoft.AspNetCore.Mvc;
using CharityHub.Application.Services.Authentications.Commands.Handlers.Device;
using CharityHub.Application.Services.Authentications.Commands.Handlers.SignIn;
using CharityHub.Application.Services.Authentications.Queries.Handlers.ValidateAccessToken;

namespace CharityHub.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {

        [HttpPost("Sign-In")]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromForm] DeviceCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("Is-Valid_Token")]
        public async Task<IActionResult> IsValidToken([FromQuery] AccessTokenQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

    }
}
