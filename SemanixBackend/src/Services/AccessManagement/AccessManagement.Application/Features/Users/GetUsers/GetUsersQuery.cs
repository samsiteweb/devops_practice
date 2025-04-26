using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AccessManagement.Application.Features.Users.GetUsers;

public class GetUsersQuery : IRequest<List<UserDto>>, IQuery
{
    public GetUsersQuery()
    {
    }
}