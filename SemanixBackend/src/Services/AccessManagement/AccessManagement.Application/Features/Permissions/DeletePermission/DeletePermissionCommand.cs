using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.DeletePermission;

public class DeletePermissionCommand : IRequest, ICommand
{
    public DeletePermissionCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}