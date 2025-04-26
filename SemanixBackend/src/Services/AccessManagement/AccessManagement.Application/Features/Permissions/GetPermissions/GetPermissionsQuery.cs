using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.GetPermissions;

public class GetPermissionsQuery : IRequest<List<PermissionDto>>, IQuery
{
    public GetPermissionsQuery()
    {
    }
}