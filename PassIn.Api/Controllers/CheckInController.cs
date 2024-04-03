using Microsoft.AspNetCore.Mvc;
using PassIn.Application.DTOs.Responses;
using PassIn.Application.UseCases.CheckIns.RegisterAttendeeCheckIn;
using PassIn.Domain.Repositories.Interfaces;

namespace PassIn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckInController : ControllerBase
{
    private readonly ICheckInRepository _checkInRepository;
    public CheckInController(ICheckInRepository checkInRepository)
    {
        _checkInRepository = checkInRepository;
    }
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [Route("{attendeeId}")]
    public async Task<IActionResult> CheckIn([FromRoute] Guid attendeeId)
    {
        var useCase = new RegisterAttendeeCheckInUseCase(_checkInRepository);
        var response = await useCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}
