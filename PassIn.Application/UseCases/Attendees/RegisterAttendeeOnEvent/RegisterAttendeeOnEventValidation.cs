using FluentValidation;
using PassIn.Application.DTOs.Requests;

namespace PassIn.Application.UseCases.Attendees.RegisterAttendeeOnEvent;

public class RegisterAttendeeOnEventValidation : AbstractValidator<RequestRegisterAttendeesEventJson>
{
    public RegisterAttendeeOnEventValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null.")
            .NotEmpty().WithMessage("Name cannot be empty")
            .Length(3, 20).WithMessage("Name needs to be between 3 and 20 characters");

        RuleFor(x => x.Email)
            .EmailAddress();
    }
}
