using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;

public interface IEventsRepository
{
    Task<Event?> GetEventByIdAsync(Guid id);
    Task CreateEventAsync(Event eventEntity);
}
