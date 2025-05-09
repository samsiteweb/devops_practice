using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.GetPermissions;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetPermissionsQueryValidator : AbstractValidator<GetPermissionsQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetPermissionsQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}