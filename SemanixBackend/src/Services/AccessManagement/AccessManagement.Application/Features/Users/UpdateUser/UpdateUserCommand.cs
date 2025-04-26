using AccessManagement.Application.Common.Interfaces;
using AccessManagement.Domain;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Users.UpdateUser;

public class UpdateUserCommand : IRequest, ICommand
{
    public UpdateUserCommand(string firstName, string lastName, string emailAddress, Gender gender, Guid id)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        Gender = gender;
        Id = id;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public Gender Gender { get; set; }
    public Guid Id { get; set; }
}