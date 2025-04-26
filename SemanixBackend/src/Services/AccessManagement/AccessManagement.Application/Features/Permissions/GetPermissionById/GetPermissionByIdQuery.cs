using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.GetPermissionById;

public class GetPermissionByIdQuery : IRequest<PermissionDto>, IQuery
{
    public GetPermissionByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}