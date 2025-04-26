using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles.UpdateRole;

public class UpdateRoleCommand : IRequest, ICommand
{
    public UpdateRoleCommand(string roleName, string roleCode, string roleDescription, Guid id)
    {
        RoleName = roleName;
        RoleCode = roleCode;
        RoleDescription = roleDescription;
        Id = id;
    }

    public string RoleName { get; set; }
    public string RoleCode { get; set; }
    public string RoleDescription { get; set; }
    public Guid Id { get; set; }
}