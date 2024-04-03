using PassIn.Application.DTOs.Responses;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase
{
    private readonly IEventsRepository _repository;
    public GetEventByIdUseCase(IEventsRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResponseEventJson> Execute(Guid id)
    {
        var entityEvent = await _repository.GetEventByIdAsync(id);

        return entityEvent is null
            ? throw new NotFoundException("An Event with this ID doesnt exist")
            : new ResponseEventJson
        {
            Id = entityEvent.Id,
            Title = entityEvent.Title,
            Details = entityEvent.Details,
            MaximumAttendees = entityEvent.Maximum_Attendees,
            AttendeesAmount = entityEvent.Attendees.Count
            };
    }
}
