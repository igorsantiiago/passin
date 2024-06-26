﻿using Microsoft.AspNetCore.Mvc;
using PassIn.Application.DTOs.Requests;
using PassIn.Application.DTOs.Responses;
using PassIn.Application.UseCases.Attendees.GetAllAttendeeByEventId;
using PassIn.Application.UseCases.Attendees.RegisterAttendeeOnEvent;
using PassIn.Domain.Repositories.Interfaces;

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
    [Route("event/register/{eventId}")]
    public async Task<IActionResult> RegisterAttendeer([FromBody] RequestRegisterAttendeesEventJson request, [FromRoute] Guid eventId)
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        var response = await useCase.Execute(eventId, request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Route("{eventId}")]
    public async Task<IActionResult> GetAllEventAttendees([FromRoute] Guid eventId)
    {
        var useCase = new GetAllAttendeeByEventIdUseCase(_attendeesRepository);
        var response = await useCase.Execute(eventId);
        return Ok(response);
    }
}
