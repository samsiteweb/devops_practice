using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles.DeleteRole;

public class DeleteRoleCommand : IRequest, ICommand
{
    public DeleteRoleCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}