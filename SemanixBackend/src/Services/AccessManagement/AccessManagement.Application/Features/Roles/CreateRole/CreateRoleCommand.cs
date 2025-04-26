using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles.CreateRole;

public class CreateRoleCommand : IRequest<Guid>, ICommand
{
    public CreateRoleCommand(string roleName, string roleCode, string roleDescription)
    {
        RoleName = roleName;
        RoleCode = roleCode;
        RoleDescription = roleDescription;
    }

    public string RoleName { get; set; }
    public string RoleCode { get; set; }
    public string RoleDescription { get; set; }
}