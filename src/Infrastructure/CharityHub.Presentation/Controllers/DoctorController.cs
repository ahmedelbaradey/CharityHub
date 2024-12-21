using CharityHub.Presentation.Bases;
using CharityHub.Application.Services.Doctors.Commands.Handlers.AddDoctor;
using CharityHub.Application.Services.Doctors.Commands.Handlers.DeleteDoctor;
using CharityHub.Application.Services.Doctors.Commands.Handlers.EditDoctor;
using CharityHub.Application.Services.Doctors.Queries.Handlers.GetDoctorById;
using CharityHub.Application.Services.Doctors.Queries.Handlers.GetDoctorsList;
using CharityHub.Application.Services.Doctors.Queries.Handlers.GetDoctorsPaginated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace  CharityHub.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="admin")]
    public class DoctorController : AppControllerBase
    {
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDoctorsList()
        {
            var response = await Mediator.Send(new GetDoctorsListQuery());
            return NewResult(response);
        }

        [HttpGet("Paginated/List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDoctorsList([FromQuery] GetDoctorsPaginatedQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDoctorById([FromRoute] int Id)
        {
            var response = await Mediator.Send(new GetDoctorByIdQuery(Id));
            return NewResult(response);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateDoctor([FromBody] AddDoctorCommand command,CancellationToken cancellationToken)
        {
           // var command = new AddDoctorCommand() { AddDoctorDto = addDoctorDto };
            var response = await Mediator.Send(command, cancellationToken);
            return NewResult(response);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateDoctor([FromBody] EditDoctorCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            var command = new DeleteDoctorCommand { Id = id };
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
