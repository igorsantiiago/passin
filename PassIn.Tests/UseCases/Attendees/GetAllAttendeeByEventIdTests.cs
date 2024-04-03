using PassIn.Application.UseCases.Attendees.GetAllAttendeeByEventId;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Tests.FakeRepositories;

namespace PassIn.Tests.UseCases.Attendees;

public class GetAllAttendeeByEventIdTests
{
    private readonly Guid _eventId = Guid.NewGuid();
    private readonly List<Attendee> _attendees = new()
    {
        new() { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Created_At = DateOnly.FromDateTime(DateTime.Now) },
        new() { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane@example.com", Created_At = DateOnly.FromDateTime(DateTime.Now) },
    };

    [Fact]
    public async Task Should_Fail_When_Attendees_Not_Found()
    {
        var attendeesRepository = new FakeAttendeesRepository(null!);
        var useCase = new GetAllAttendeeByEventIdUseCase(attendeesRepository);
        await Assert.ThrowsAsync<NotFoundException>(() => useCase.Execute(_eventId));
    }

    [Fact]
    public async Task Should_Succeed_When_Attendees_Found()
    {
        var attendeesRepository = new FakeAttendeesRepository(_attendees);
        var useCase = new GetAllAttendeeByEventIdUseCase(attendeesRepository);
        var result = await useCase.Execute(_eventId);

        Assert.NotNull(result);
        Assert.NotNull(result.Attendees);
        Assert.Equal(2, result.Attendees.Count);

        Assert.Equal(_attendees[0].Id, result.Attendees[0].Id);
        Assert.Equal(_attendees[0].Name, result.Attendees[0].Name);
        Assert.Equal(_attendees[0].Email, result.Attendees[0].Email);
        Assert.Equal(_attendees[0].Created_At, result.Attendees[0].CreatedAt);
        Assert.Null(result.Attendees[0].CheckedInAt);

        Assert.Equal(_attendees[1].Id, result.Attendees[1].Id);
        Assert.Equal(_attendees[1].Name, result.Attendees[1].Name);
        Assert.Equal(_attendees[1].Email, result.Attendees[1].Email);
        Assert.Equal(_attendees[1].Created_At, result.Attendees[1].CreatedAt);
        Assert.Null(result.Attendees[1].CheckedInAt);
    }
}
