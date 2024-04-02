using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.RegisterAttendeeOnEvent;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Repositories.UseCases.Interfaces;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    private readonly IAttendeesRepository _attendeesRepository;
    public AttendeesController(IAttendeesRepository attendeesRepository)
    {
        _attendeesRepository = attendeesRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseAttendeeRegisteredOnEvent), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [Route("event/{eventId}")]
    public async Task<IActionResult> RegisterAttendeer([FromBody] RequestRegisterAttendeesEventJson request, [FromRoute] Guid eventId)
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        var response = await useCase.Execute(eventId, request);
        return Created(string.Empty, response);
    }
}
