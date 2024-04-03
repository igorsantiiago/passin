using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;

public interface ICheckInRepository
{
    Task<bool> CheckAttendeeExists(Guid attendeeId);
    Task<bool> CheckInExists(Guid attendeeId);
    Task AddNewCheckIn(CheckIn checkIn);
}
