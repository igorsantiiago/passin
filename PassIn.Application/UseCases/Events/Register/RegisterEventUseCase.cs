using FluentValidation.Results;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Repositories.UseCases.Interfaces;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    private readonly IEventsRepository _repository;
    private readonly RegisterEventValidation _validation = new();
    public RegisterEventUseCase(IEventsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseRegisteredEventJson> Execute(RequestEventJson request)
    {
        ValidationResult result = _validation.Validate(request);
        if (!result.IsValid)
            throw new ValidationErrorException(result.ToString());

        var entityEvent = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.Replace(" ", "-").ToLower()
        };

        await _repository.CreateEventAsync(entityEvent);

        return new ResponseRegisteredEventJson
        {
            Id = entityEvent.Id
        };

    }
}
