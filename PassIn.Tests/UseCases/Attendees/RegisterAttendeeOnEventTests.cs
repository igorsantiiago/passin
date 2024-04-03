using PassIn.Application.DTOs.Requests;
using PassIn.Application.UseCases.Attendees.RegisterAttendeeOnEvent;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Tests.FakeRepositories;

namespace PassIn.Tests.UseCases.Attendees;

public class RegisterAttendeeOnEventTests
{
    private readonly FakeAttendeesRepository _attendeesRepository;
    private readonly Guid _validEventId = new("4a820e45-9853-4f02-b392-2141510b6ef7");

    private readonly RequestRegisterAttendeesEventJson _request = new() { Name = "usernlw", Email = "usernlw@teste.com" };
    private readonly RequestRegisterAttendeesEventJson _invalidNullNameRequest = new() { Name = null!, Email = "user01@teste.com" };
    private readonly RequestRegisterAttendeesEventJson _invalidEmptyNameRequest = new() { Name = "", Email = "user01@teste.com" };
    private readonly RequestRegisterAttendeesEventJson _invalidShortNameRequest = new() { Name = "a", Email = "user01@teste.com" };
    private readonly RequestRegisterAttendeesEventJson _invalidLargeNameRequest = new() { Name = "user01user01user01user01", Email = "user01@teste.com" };
    private readonly RequestRegisterAttendeesEventJson _invalidEmailRequest = new() { Name = "user01", Email = "userteste.com" };
    private readonly RequestRegisterAttendeesEventJson _invalidAttendeeAlreadyExistsRequest = new() { Name = "John Doe", Email = "john@example.com" };

    private readonly List<Attendee> _attendees =
    [
        new() { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Created_At = DateOnly.FromDateTime(DateTime.Now) },
        new() { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane@example.com", Created_At = DateOnly.FromDateTime(DateTime.Now) },
    ];

    public RegisterAttendeeOnEventTests()
    {
        _attendeesRepository = new FakeAttendeesRepository(_attendees);
    }

    [Fact]
    public async Task Should_Fail_When_Event_Not_Found()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<NotFoundException>(async() => await useCase.Execute(Guid.NewGuid(), _request));
    }

    [Fact]
    public async Task Should_Fail_When_Name_Validation_Is_Null()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<ValidationErrorException>(async () => await useCase.Execute(_validEventId, _invalidNullNameRequest));
    }

    [Fact]
    public async Task Should_Fail_When_Name_Is_Empty()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<ValidationErrorException>(async () => await useCase.Execute(_validEventId, _invalidEmptyNameRequest));
    }

    [Fact]
    public async Task Should_Fail_When_Name_Has_Less_Than_3_Digits()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<ValidationErrorException>(async () => await useCase.Execute(_validEventId, _invalidShortNameRequest));
    }

    [Fact]
    public async Task Should_Fail_When_Name_Has_More_Than_20_Digits()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<ValidationErrorException>(async () => await useCase.Execute(_validEventId, _invalidLargeNameRequest));
    }

    [Fact]
    public async Task Should_Fail_When_Email_Is_Invalid()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<ValidationErrorException>(async () => await useCase.Execute(_validEventId, _invalidEmailRequest));
    }

    [Fact]
    public async Task Should_Fail_When_Attendee_Already_Registered()
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);
        await Assert.ThrowsAsync<ConflictException>(async () => await useCase.Execute(_validEventId, _invalidAttendeeAlreadyExistsRequest));
    }

    [Fact]
    public async Task Should_Fail_When_Event_Is_Full_Booked()
    {
        var maxAttendees = 10;
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository);

        for (int i = 0; i < maxAttendees; i++)
            await _attendeesRepository.AddAttendeeAsync(new Attendee { Name = $"user{i}", Email = $"teste{i}@teste{i}.com", Event_Id = _validEventId });

        await Assert.ThrowsAsync<ForbiddenException>(async () => await useCase.Execute(_validEventId, _request));
    }

    [Fact]
    public async Task Should_Succeed_When_Attendee_Registered_To_Event()
    {
        var attendeesRepository = new FakeAttendeesRepository(new List<Attendee>());
        var useCase = new RegisterAttendeeOnEventUseCase(attendeesRepository);
        var response = await useCase.Execute(_validEventId, _request);

        Assert.NotNull(response);
        Assert.Equal(_request.Name, response.Name);
        Assert.Equal(_request.Email, response.Email);
        Assert.Equal(_validEventId, response.EventId);
    }
}


