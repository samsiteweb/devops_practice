using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.UpdateRole;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    [IntentManaged(Mode.Merge)]
    public UpdateRoleCommandValidator()
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