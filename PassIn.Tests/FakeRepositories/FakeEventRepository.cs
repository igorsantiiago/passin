using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;

namespace PassIn.Tests.FakeRepositories;

public class FakeEventRepository : IEventsRepository
{
    private readonly Dictionary<Guid, Event> _events = new Dictionary<Guid, Event>();
    private readonly Event _event = new Event()
    {
        Id = new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"),
        Title = "Event NLW",
        Details = "Event Description",
        Maximum_Attendees = 10,
        Slug = "event_nlw"
    };

    public Task CreateEventAsync(Event eventEntity)
    {
        _events[eventEntity.Id] = eventEntity;
        return Task.CompletedTask;
    }

    public Task<Event?> GetEventByIdAsync(Guid id)
    {
        if (id == _event.Id)
            return Task.FromResult<Event?>(_event);

        return Task.FromResult<Event?>(null);
    }
}
