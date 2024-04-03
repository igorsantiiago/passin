using Microsoft.Extensions.Logging;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using System.Xml.Linq;

namespace PassIn.Tests.FakeRepositories;

public class FakeCheckInRepository : ICheckInRepository
{
    private readonly Dictionary<Guid, CheckIn> _checkIns;
    private readonly Event _event;
    private readonly Attendee _attendeeExists;
    private readonly Attendee _validAttendeeForCheckIn;

    public FakeCheckInRepository()
    {
        _checkIns = new Dictionary<Guid, CheckIn>();

        _event = new Event()
        {
            Id = new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"),
            Title = "Event NLW",
            Details = "Event Description",
            Maximum_Attendees = 10,
            Slug = "event_nlw"
        };

        _attendeeExists = new Attendee()
        {
            Id = new Guid("6f3b6d8e-3e4c-4fcf-8dd8-f61c0165a259"),
            Name = "UserNlwTeste",
            Email = "usernlwteste@teste.com",
            Created_At = DateOnly.FromDateTime(DateTime.UtcNow),
            Event_Id = new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"),
            CheckIn = new CheckIn()
            {
                Id = new Guid("1cf11bb4-96ec-4396-baeb-ec5b72d9a9f7"),
                Attendee_Id = new Guid("6f3b6d8e-3e4c-4fcf-8dd8-f61c0165a259")
            }
        };

        _validAttendeeForCheckIn = new Attendee()
        {
            Id = new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"),
            Name = "ValidCheckIn",
            Email = "validcheckin@email.com",
            Created_At = DateOnly.FromDateTime(DateTime.UtcNow),
            Event_Id = _event.Id
        };
    }

    public async Task AddNewCheckIn(CheckIn checkIn)
    {
        _checkIns[checkIn.Id] = checkIn;
        await Task.CompletedTask;
    }

    public Task<bool> CheckAttendeeExists(Guid attendeeId)
    {
        if(attendeeId == _attendeeExists.Id)
            return Task.FromResult(true);
        else if(attendeeId == _validAttendeeForCheckIn.Id)
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    public Task<bool> CheckInExists(Guid attendeeId)
    {
        if (_attendeeExists.CheckIn != null && _attendeeExists.CheckIn.Attendee_Id == attendeeId)
            return Task.FromResult(true);

        return Task.FromResult(false);
    }
}
