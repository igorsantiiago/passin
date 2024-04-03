using PassIn.Application.DTOs.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.CheckIns.RegisterAttendeeCheckIn;

public class RegisterAttendeeCheckInUseCase
{
    private readonly ICheckInRepository _checkInRepository;
    public RegisterAttendeeCheckInUseCase(ICheckInRepository checkInRepository)
    {
        _checkInRepository = checkInRepository;
    }

    public async Task<ResponseRegisteredEventJson> Execute(Guid attendeeId)
    {
        var attendeeExists = await _checkInRepository.CheckAttendeeExists(attendeeId);
        if (!attendeeExists)
            throw new NotFoundException("An attendee with this ID was not found.");

        var checkInExists = await _checkInRepository.CheckInExists(attendeeId);
        if (checkInExists)
            throw new ConflictException("An attendee cannot do CheckIn twice in the same event.");

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        await _checkInRepository.AddNewCheckIn(entity);

        return new ResponseRegisteredEventJson
        {
            Id = entity.Id
        };
    }
}
