using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;

public interface IAttendeesRepository
{
    Task<Event?> GetEventAsync(Guid id);
    Task<bool> IsAttendeeAlreadyRegisteredAsync(Guid eventId, string email);
    Task<int> GetEventAttendeesCountAsync(Guid eventId);
    Task AddAttendeeAsync(Attendee attendee);
    Task<Event?> GetAllAttendeesWithCheckInByEventId(Guid eventId);
}
