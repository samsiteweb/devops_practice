using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.DeleteUserGroups;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class DeleteUserGroupsCommandValidator : AbstractValidator<DeleteUserGroupsCommand>
{
    [IntentManaged(Mode.Merge)]
    public DeleteUserGroupsCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}