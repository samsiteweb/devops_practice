using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.GetUserGroupsById;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class GetUserGroupsByIdQueryHandler : IRequestHandler<GetUserGroupsByIdQuery, UserGroupsDto>
{
    private readonly IUserGroupsRepository _userGroupsRepository;
    private readonly IMapper _mapper;

    [IntentManaged(Mode.Merge)]
    public GetUserGroupsByIdQueryHandler(IUserGroupsRepository userGroupsRepository, IMapper mapper)
    {
        _userGroupsRepository = userGroupsRepository;
        _mapper = mapper;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<UserGroupsDto> Handle(GetUserGroupsByIdQuery request, CancellationToken cancellationToken)
    {
        var userGroups = await _userGroupsRepository.FindByIdAsync(request.Id, cancellationToken);
        if (userGroups is null)
        {
            throw new NotFoundException($"Could not find UserGroups '{request.Id}'");
        }

        return userGroups.MapToUserGroupsDto(_mapper);
    }
}