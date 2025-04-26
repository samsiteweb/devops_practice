using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.CreatePermission;

public class CreatePermissionCommand : IRequest<Guid>, ICommand
{
    public CreatePermissionCommand(string permissionName, string permissionCode, string permissionDescription)
    {
        PermissionName = permissionName;
        PermissionCode = permissionCode;
        PermissionDescription = permissionDescription;
    }

    public string PermissionName { get; set; }
    public string PermissionCode { get; set; }
    public string PermissionDescription { get; set; }
}