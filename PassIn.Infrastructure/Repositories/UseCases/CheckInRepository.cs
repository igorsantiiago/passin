using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Repositories.UseCases.Interfaces;

namespace PassIn.Infrastructure.Repositories.UseCases;

public class CheckInRepository : ICheckInRepository
{
    private readonly PassInDbContext _dbContext;

    public CheckInRepository(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CheckAttendeeExists(Guid attendeeId)
        => await _dbContext.Attendees.AnyAsync(x => x.Id == attendeeId);

    public async Task<bool> CheckInExists(Guid attendeeId)
        => await _dbContext.CheckIns.AnyAsync(x => x.Attendee_Id == attendeeId);

    public async Task AddNewCheckIn(CheckIn checkIn)
    {
        await _dbContext.CheckIns.AddAsync(checkIn);
        await _dbContext.SaveChangesAsync();
    }
}
