using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;

namespace PassIn.Tests.FakeRepositories;

public class FakeAttendeesRepository : IAttendeesRepository
{
    private readonly List<Attendee> _attendees;

    private readonly Event _event = new Event()
    {
        Id = new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"),
        Title = "Event NLW",
        Details = "Event Description",
        Maximum_Attendees = 10,
        Slug = "event_nlw"
    };

    public FakeAttendeesRepository(List<Attendee> attendees)
    {
        _attendees = attendees;
    }

    public Task AddAttendeeAsync(Attendee attendee)
    {
        _attendees.Add(attendee);
        return Task.CompletedTask;
    }

    public Task<Event?> GetAllAttendeesWithCheckInByEventId(Guid eventId)
    {
        if (_attendees == null)
            return Task.FromResult<Event?>(null);

        return Task.FromResult<Event?>(new Event { Attendees = _attendees.ToList() });
    }

    public Task<Event?> GetEventAsync(Guid id)
    {
        if (id == _event.Id)
            return Task.FromResult<Event?>(_event);

        return Task.FromResult<Event?>(null);
    }

    public Task<int> GetEventAttendeesCountAsync(Guid eventId)
    {
        var attendeesCount = _attendees.Count(a => a.Event_Id == eventId);
        return Task.FromResult(attendeesCount);
    }

    public Task<bool> IsAttendeeAlreadyRegisteredAsync(Guid eventId, string email)
    {
        if (_attendees.Any(x => x.Email == email))
            return Task.FromResult(true);

        return Task.FromResult(false);
    }
}
