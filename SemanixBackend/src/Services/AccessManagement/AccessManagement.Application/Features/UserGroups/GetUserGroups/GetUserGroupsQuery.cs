using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.GetUserGroups;

public class GetUserGroupsQuery : IRequest<List<UserGroupsDto>>, IQuery
{
    public GetUserGroupsQuery()
    {
    }
}