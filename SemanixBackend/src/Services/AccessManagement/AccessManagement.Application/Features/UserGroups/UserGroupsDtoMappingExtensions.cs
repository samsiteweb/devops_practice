using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups;

public static class UserGroupsDtoMappingExtensions
{
    public static UserGroupsDto MapToUserGroupsDto(this Domain.Entities.UserGroups projectFrom, IMapper mapper)
        => mapper.Map<UserGroupsDto>(projectFrom);

    public static List<UserGroupsDto> MapToUserGroupsDtoList(this IEnumerable<Domain.Entities.UserGroups> projectFrom, IMapper mapper)
        => projectFrom.Select(x => x.MapToUserGroupsDto(mapper)).ToList();
}