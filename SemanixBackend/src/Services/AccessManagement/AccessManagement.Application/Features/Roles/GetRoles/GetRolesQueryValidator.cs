using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.GetRoles;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetRolesQueryValidator : AbstractValidator<GetRolesQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetRolesQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}