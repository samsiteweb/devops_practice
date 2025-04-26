using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Users.UpdateUser;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    [IntentManaged(Mode.Merge)]
    public UpdateUserCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        RuleFor(v => v.FirstName)
            .NotNull();

        RuleFor(v => v.LastName)
            .NotNull();

        RuleFor(v => v.EmailAddress)
            .NotNull();

        RuleFor(v => v.Gender)
            .NotNull()
            .IsInEnum();
    }
}