using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Repositories.UseCases.Interfaces;

namespace PassIn.Infrastructure.Repositories.UseCases;

public class EventsRepository : IEventsRepository
{
    private readonly PassInDbContext _dbContext;
    public EventsRepository(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Event?> GetEventByIdAsync(Guid id)
        => await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

    public async Task CreateEventAsync(Event eventEntity)
    {
        await _dbContext.Events.AddAsync(eventEntity);
        await _dbContext.SaveChangesAsync();
    }
}
