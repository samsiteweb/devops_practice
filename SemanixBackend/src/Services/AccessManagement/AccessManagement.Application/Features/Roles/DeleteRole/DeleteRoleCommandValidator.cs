using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.DeleteRole;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    [IntentManaged(Mode.Merge)]
    public DeleteRoleCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}