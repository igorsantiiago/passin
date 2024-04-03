using PassIn.Application.UseCases.CheckIns.RegisterAttendeeCheckIn;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Tests.FakeRepositories;

namespace PassIn.Tests.UseCases.CheckIns;

public class RegisterAttendeeCheckInTests
{
    private readonly Guid _checkedTwiceAttendeeId = new Guid("6f3b6d8e-3e4c-4fcf-8dd8-f61c0165a259");
    private readonly Guid _invalidAttendeeId = Guid.NewGuid();

    private readonly FakeCheckInRepository _checkInRepository;

    public RegisterAttendeeCheckInTests()
    {
        _checkInRepository = new FakeCheckInRepository();
    }

    [Fact]
    public async Task Should_Fail_When_Attendee_Not_Found()
    {
        var useCase = new RegisterAttendeeCheckInUseCase(_checkInRepository);
        await Assert.ThrowsAsync<NotFoundException>(async () => await useCase.Execute(_invalidAttendeeId));
    }

    [Fact]
    public async Task Should_Fail_When_Attendee_CheckIn_Twice_At_Same_Event()
    {
        var useCase = new RegisterAttendeeCheckInUseCase(_checkInRepository);
        await Assert.ThrowsAsync<ConflictException>(async () => await useCase.Execute(_checkedTwiceAttendeeId));
    }


    [Fact]
    public async Task Should_Succeed_When_CheckIn()
    {
        var useCase = new RegisterAttendeeCheckInUseCase(_checkInRepository);
        var checkin = await useCase.Execute(new Guid("4a820e45-9853-4f02-b392-2141510b6ef7"));
        Assert.NotNull(checkin);
    }
}