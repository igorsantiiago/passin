using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure.Repositories.UseCases.Interfaces;

public interface ICheckInRepository
{
    Task<bool> CheckAttendeeExists(Guid attendeeId);
    Task<bool> CheckInExists(Guid attendeeId);
    Task AddNewCheckIn(CheckIn checkIn);
}
