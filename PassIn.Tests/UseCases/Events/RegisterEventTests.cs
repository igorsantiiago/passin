using PassIn.Application.DTOs.Requests;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Tests.FakeRepositories;

namespace PassIn.Tests.UseCases.Events;

public class RegisterEventTests
{
    private readonly RequestEventJson _invalidEventWithTitleNull;
    private readonly RequestEventJson _invalidEventWithTitleEmpty;
    private readonly RequestEventJson _invalidEventWithTitleShort;
    private readonly RequestEventJson _invalidEventWithTitleLarge;
    private readonly RequestEventJson _invalidEventWithDetailsNull;
    private readonly RequestEventJson _invalidEventWithDetailsEmpty;
    private readonly RequestEventJson _invalidEventWithDetailsShort;
    private readonly RequestEventJson _invalidEventWithDetailsLarge;
    private readonly RequestEventJson _invalidEventWithMaxAttendeesZero;
    private readonly RequestEventJson _invalidEventWithMaxAttendeesNegative;
    private readonly RequestEventJson _validEvent;

    private readonly FakeEventRepository _eventRepository;

    public RegisterEventTests()
    {
        _invalidEventWithTitleNull = new RequestEventJson { Title = null!, Details = "Event Description", MaximumAttendees = 10,};
        _invalidEventWithTitleEmpty = new RequestEventJson { Title = "", Details = "Event Description", MaximumAttendees = 10 };
        _invalidEventWithTitleShort = new RequestEventJson { Title = "A", Details = "Event Description", MaximumAttendees = 10 };
        _invalidEventWithTitleLarge = new RequestEventJson { Title = new string('A', 251), Details = "Event Description", MaximumAttendees = 10 };
        _invalidEventWithDetailsNull = new RequestEventJson { Title = "Event Title", Details = null!, MaximumAttendees = 10 };
        _invalidEventWithDetailsEmpty = new RequestEventJson { Title = "Event Title", Details = "", MaximumAttendees = 10 };
        _invalidEventWithDetailsShort = new RequestEventJson { Title = "Event Title", Details = "A", MaximumAttendees = 10 };
        _invalidEventWithDetailsLarge = new RequestEventJson { Title = "Event Title", Details = new string('A', 1001), MaximumAttendees = 10 };
        _invalidEventWithMaxAttendeesZero = new RequestEventJson { Title = "Event Title", Details = "Event Description", MaximumAttendees = 0 };
        _invalidEventWithMaxAttendeesNegative = new RequestEventJson { Title = "Event Title", Details = "Event Description", MaximumAttendees = -5, };
        _validEvent = new RequestEventJson { Title = "Event Title", Details = "Event Description", MaximumAttendees = 10 };

        _eventRepository = new FakeEventRepository();
    }

        

    [Fact]
    public async Task Should_Fail_When_Title_Is_Null()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithTitleNull);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Title_Is_Empty()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithTitleEmpty);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Title_Is_Short()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithTitleShort);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Title_Is_Large()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithTitleLarge);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Details_Is_Null()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithDetailsNull);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Details_Is_Empty()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithDetailsEmpty);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Details_Is_Short()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithDetailsShort);
        });
    }

    [Fact]
    public async Task Should_Fail_When_Details_Is_Large()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithDetailsLarge);
        });
    }

    [Fact]
    public async Task Should_Fail_When_MaximumAttendee_Equals_Zero()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithMaxAttendeesZero);
        });
    }

    [Fact]
    public async Task Should_Fail_When_MaximumAttendee_Under_Zero()
    {
        await Assert.ThrowsAsync<ValidationErrorException>(async () =>
        {
            var useCase = new RegisterEventUseCase(_eventRepository);
            await useCase.Execute(_invalidEventWithMaxAttendeesNegative );
        });
    }

    [Fact]
    public async Task Should_Succeed_When_Event_Created()
    {
        var useCase = new RegisterEventUseCase(_eventRepository);
        var result = await useCase.Execute(_validEvent);

        Assert.NotNull(result);
    }
}
