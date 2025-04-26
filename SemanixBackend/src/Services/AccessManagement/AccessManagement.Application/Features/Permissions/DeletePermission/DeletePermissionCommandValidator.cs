using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.DeletePermission;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    [IntentManaged(Mode.Merge)]
    public DeletePermissionCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}