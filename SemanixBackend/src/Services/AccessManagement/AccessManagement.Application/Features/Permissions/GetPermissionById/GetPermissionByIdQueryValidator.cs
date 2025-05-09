using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.GetPermissionById;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetPermissionByIdQueryValidator : AbstractValidator<GetPermissionByIdQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetPermissionByIdQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}