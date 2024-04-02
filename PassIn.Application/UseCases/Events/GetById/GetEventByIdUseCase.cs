using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetEventByIdUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ResponseEventJson> Execute(Guid id)
    {
        var entityEvent = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

        return entityEvent is null
            ? throw new PassInException("An Event with this ID doesnt exist")
            : new ResponseEventJson
        {
            Id = entityEvent.Id,
            Title = entityEvent.Title,
            Details = entityEvent.Details,
            MaximumAttendees = entityEvent.Maximum_Attendees,
            AttendeesAmount = -1
        };
    }
}
