using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.UpdatePermission;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    [IntentManaged(Mode.Merge)]
    public UpdatePermissionCommandValidator()
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