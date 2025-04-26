using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.GetUserGroupsById;

public class GetUserGroupsByIdQuery : IRequest<UserGroupsDto>, IQuery
{
    public GetUserGroupsByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}