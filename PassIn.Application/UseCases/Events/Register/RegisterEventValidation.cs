using FluentValidation;
using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventValidation : AbstractValidator<RequestEventJson>
{
    public RegisterEventValidation()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title cannot be null.")
            .NotEmpty().WithMessage("Title cannot be empty")
            .Length(3, 30).WithMessage("Title needs to be between 3 and 30 characters");

        RuleFor(x => x.Details)
            .NotNull().WithMessage("Details cannot be null.")
            .NotEmpty().WithMessage("Details cannot be empty")
            .Length(6, 60).WithMessage("Details needs to be between 6 and 60 characters");

        RuleFor(x => x.MaximumAttendees)
            .GreaterThan(0).WithMessage("Maximum Attendees needs to be greater than zero.");
    }
}
