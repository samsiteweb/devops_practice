using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.CreateRole;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    [IntentManaged(Mode.Merge)]
    public CreateRoleCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        RuleFor(v => v.RoleName)
            .NotNull();

        RuleFor(v => v.RoleCode)
            .NotNull();

        RuleFor(v => v.RoleDescription)
            .NotNull();
    }
}