using FluentValidation.Results;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    private readonly PassInDbContext _dbContext;
    private readonly RegisterEventValidation _validation = new();
    public RegisterEventUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseRegisteredEventJson> Execute(RequestEventJson request)
    {
        ValidationResult result = _validation.Validate(request);
        if (!result.IsValid)
            throw new PassInException(result.ToString());

        var entityEvent = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.Replace(" ", "-").ToLower()
        };

        await _dbContext.Events.AddAsync(entityEvent);
        await _dbContext.SaveChangesAsync();

        return new ResponseRegisteredEventJson
        {
            Id = entityEvent.Id
        };

    }
}
