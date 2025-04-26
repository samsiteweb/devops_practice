using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.GetUserGroupsById;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetUserGroupsByIdQueryValidator : AbstractValidator<GetUserGroupsByIdQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetUserGroupsByIdQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}