using Microsoft.AspNetCore.Mvc;
using PassIn.Application.DTOs.Requests;
using PassIn.Application.DTOs.Responses;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Domain.Repositories.Interfaces;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventsRepository _eventsRepository;
    public EventController(IEventsRepository repository)
    {
        _eventsRepository = repository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RequestEventJson request)
    {
        var useCase = new RegisterEventUseCase(_eventsRepository);
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var useCase = new GetEventByIdUseCase(_eventsRepository);
        var response = await useCase.Execute(id);

        return Ok(response);
    }
}
