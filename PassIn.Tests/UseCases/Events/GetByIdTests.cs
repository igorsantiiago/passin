using Microsoft.Extensions.Logging;
using PassIn.Application.UseCases.CheckIns.RegisterAttendeeCheckIn;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Tests.FakeRepositories;

namespace PassIn.Tests.UseCases.Events;

public class GetByIdTests
{
    private readonly Event _newEvent = new Event()
    {
        Id = new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"),
        Title = "Event NLW",
        Details = "Event Description",
        Maximum_Attendees = 10,
        Slug = "event_nlw"
    };

    private readonly FakeEventRepository _eventRepository;

    public GetByIdTests()
    {
        _eventRepository = new FakeEventRepository();
    }

    [Fact]
    public async Task Should_Fail_When_Event_Not_Found()
    {
        var useCase = new GetEventByIdUseCase(_eventRepository);
        await Assert.ThrowsAsync<NotFoundException>(async () => await useCase.Execute(Guid.NewGuid()));
    }

    [Fact]
    public async Task Should_Succeed_When_Event_Found()
    {
        var useCase = new GetEventByIdUseCase(_eventRepository);
        var result = await useCase.Execute(new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"));

        Assert.NotNull(result);
        Assert.Equal(_newEvent.Id, result.Id);
        Assert.Equal(_newEvent.Title, result.Title);
        Assert.Equal(_newEvent.Details, result.Details);
    }
}
