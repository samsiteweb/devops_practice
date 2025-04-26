using AccessManagement.Domain.Repositories;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.GetUserGroups;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, List<UserGroupsDto>>
{
    private readonly IUserGroupsRepository _userGroupsRepository;
    private readonly IMapper _mapper;

    [IntentManaged(Mode.Merge)]
    public GetUserGroupsQueryHandler(IUserGroupsRepository userGroupsRepository, IMapper mapper)
    {
        _userGroupsRepository = userGroupsRepository;
        _mapper = mapper;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<List<UserGroupsDto>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
    {
        var userGroups = await _userGroupsRepository.FindAllAsync(cancellationToken);
        return userGroups.MapToUserGroupsDtoList(_mapper);
    }
}