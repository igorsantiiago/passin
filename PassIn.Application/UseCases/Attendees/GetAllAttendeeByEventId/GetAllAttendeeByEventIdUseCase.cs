using PassIn.Application.DTOs.Responses;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Attendees.GetAllAttendeeByEventId;

public class GetAllAttendeeByEventIdUseCase
{
    private readonly IAttendeesRepository _attendeesRepository;
    public GetAllAttendeeByEventIdUseCase(IAttendeesRepository attendeesRepository)
    {
        _attendeesRepository = attendeesRepository;
    }

    public async Task<ResponseAllAttendeesJson> Execute(Guid eventId)
    {
        var entity = await _attendeesRepository.GetAllAttendeesWithCheckInByEventId(eventId);
        if (entity is null)
            throw new NotFoundException("An event with this id was not found.");

        return new ResponseAllAttendeesJson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at
            }).ToList()
        };
    }
}
