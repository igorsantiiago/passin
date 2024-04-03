using FluentValidation.Results;
using PassIn.Application.DTOs.Requests;
using PassIn.Application.DTOs.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions;

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
