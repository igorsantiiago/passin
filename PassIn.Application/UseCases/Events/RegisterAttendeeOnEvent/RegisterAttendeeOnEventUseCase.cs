using FluentValidation.Results;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Repositories.UseCases.Interfaces;

namespace PassIn.Application.UseCases.Events.RegisterAttendeeOnEvent;

public class RegisterAttendeeOnEventUseCase
{
    private readonly IAttendeesRepository _attendeesRepository;
    private readonly RegisterAttendeeOnEventValidation _validation = new();
    public RegisterAttendeeOnEventUseCase(IAttendeesRepository attendeesRepository)
    {
        _attendeesRepository = attendeesRepository;
    }

    public async Task<ResponseAttendeeRegisteredOnEvent> Execute(Guid eventId, RequestRegisterAttendeesEventJson request)
    {
        var entityEvent = await _attendeesRepository.GetEventAsync(eventId);
        if (entityEvent is null)
            throw new NotFoundException("An event with this Id was not found.");

        ValidationResult result = _validation.Validate(request);
        if(!result.IsValid)
            throw new ValidationErrorException(result.ToString());

        var attendeeAlreadyRegistered =
            await _attendeesRepository.IsAttendeeAlreadyRegisteredAsync(eventId, request.Email);

        if (attendeeAlreadyRegistered)
            throw new ConflictException("Attendee already registered for this event.");

        var eventAttendees = await _attendeesRepository.GetEventAttendeesCountAsync(eventId);
        if (eventAttendees >= entityEvent.Maximum_Attendees)
            throw new ForbiddenException("Event is fully booked.");

        var entityAttendee = new Attendee
        {
            Name = request.Email,
            Email = request.Email,
            Event_Id = eventId,
            Created_At = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await _attendeesRepository.AddAttendeeAsync(entityAttendee);

        return new ResponseAttendeeRegisteredOnEvent
        {
            Id = entityAttendee.Id,
            Name = entityAttendee.Name,
            Email = entityAttendee.Email,
            EventId = entityAttendee.Event_Id
        };
    }
}
