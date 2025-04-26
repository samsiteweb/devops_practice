using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.CreatePermission;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    [IntentManaged(Mode.Merge)]
    public CreatePermissionCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        RuleFor(v => v.PermissionName)
            .NotNull();

        RuleFor(v => v.PermissionCode)
            .NotNull();

        RuleFor(v => v.PermissionDescription)
            .NotNull();
    }
}