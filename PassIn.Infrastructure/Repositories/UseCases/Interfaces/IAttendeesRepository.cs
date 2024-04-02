using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure.Repositories.UseCases.Interfaces;

public interface IAttendeesRepository
{
    Task<Event?> GetEventAsync(Guid id);
    Task<bool> IsAttendeeAlreadyRegisteredAsync(Guid eventId, string email);
    Task<int> GetEventAttendeesCountAsync(Guid eventId);
    Task AddAttendeeAsync(Attendee attendee);
}
