using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.GetRoleById;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetRoleByIdQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}