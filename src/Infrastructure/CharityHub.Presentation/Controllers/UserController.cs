using CharityHub.Presentation.Bases;
using CharityHub.Application.Services.Accounts.Commands.Models;
using CharityHub.Application.Services.Accounts.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharityHub.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpGet("Paginated/List")]
        public async Task<IActionResult> GetUserPaginatedList([FromQuery] GetAccountPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int Id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(Id));
            return NewResult(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddUser([FromBody] AddAccountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] EditAccountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int Id)
        {
            var response = await Mediator.Send(new DeleteAccountCommand(Id));
            return NewResult(response);
        }

        [HttpPut("Change Password")]
        public async Task<IActionResult> ChangePasswordForUser([FromBody] ChangeAccountPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
