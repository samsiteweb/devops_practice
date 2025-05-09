using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace AccessManagement.Application.Features.Users.GetUsers;

[IntentManaged(Mode.Fully, Body = Mode.Merge)]
public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    [IntentManaged(Mode.Merge)]
    public GetUsersQueryValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        // Implement custom validation logic here if required
    }
}