using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.DeleteUserGroups;

public class DeleteUserGroupsCommand : IRequest, ICommand
{
    public DeleteUserGroupsCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}