using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>>, IQuery
{
    public GetRolesQuery()
    {
    }
}