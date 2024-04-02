using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure.Repositories.UseCases.Interfaces;

public interface IEventsRepository
{
    Task<Event?> GetEventByIdAsync(Guid id);
    Task CreateEventAsync(Event eventEntity);
}
