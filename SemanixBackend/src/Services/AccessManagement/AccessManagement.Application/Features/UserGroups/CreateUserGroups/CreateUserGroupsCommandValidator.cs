using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.CreateUserGroups;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class CreateUserGroupsCommandValidator : AbstractValidator<CreateUserGroupsCommand>
{
    [IntentManaged(Mode.Merge)]
    public CreateUserGroupsCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        RuleFor(v => v.GroupName)
            .NotNull();

        RuleFor(v => v.GroupCode)
            .NotNull();

        RuleFor(v => v.GroupDescription)
            .NotNull();
    }
}