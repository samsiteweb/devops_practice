using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.UpdatePermission;

public class UpdatePermissionCommand : IRequest, ICommand
{
    public UpdatePermissionCommand(string permissionName, string permissionCode, string permissionDescription, Guid id)
    {
        PermissionName = permissionName;
        PermissionCode = permissionCode;
        PermissionDescription = permissionDescription;
        Id = id;
    }

    public string PermissionName { get; set; }
    public string PermissionCode { get; set; }
    public string PermissionDescription { get; set; }
    public Guid Id { get; set; }
}