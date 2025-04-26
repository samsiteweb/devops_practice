using AccessManagement.Application.Common.Interfaces;
using AccessManagement.Domain;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Users.CreateUser;

public class CreateUserCommand : IRequest<Guid>, ICommand
{
    public CreateUserCommand(string firstName, string lastName, string emailAddress, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        Gender = gender;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public Gender Gender { get; set; }
}