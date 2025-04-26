using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.GetUserGroups;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetUserGroupsQueryValidator : AbstractValidator<GetUserGroupsQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetUserGroupsQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}