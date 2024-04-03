using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;

namespace PassIn.Infrastructure.Repositories.UseCases;

public class AttendeesRepository : IAttendeesRepository
{
    private readonly PassInDbContext _dbContext;

    public AttendeesRepository(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Event?> GetEventAsync(Guid id)
    {
        return await _dbContext.Events.FindAsync(id);
    }

    public async Task<bool> IsAttendeeAlreadyRegisteredAsync(Guid eventId, string email)
    {
        return await _dbContext.Attendees
            .AnyAsync(x => x.Email.Equals(email) && x.Event_Id == eventId);
    }

    public async Task<int> GetEventAttendeesCountAsync(Guid eventId)
    {
        return await _dbContext.Attendees
            .CountAsync(x => x.Event_Id == eventId);
    }

    public async Task AddAttendeeAsync(Attendee attendee)
    {
        await _dbContext.Attendees.AddAsync(attendee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Event?> GetAllAttendeesWithCheckInByEventId(Guid eventId)
        => await _dbContext.Events.Include(e => e.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefaultAsync(e => e.Id == eventId);
}
