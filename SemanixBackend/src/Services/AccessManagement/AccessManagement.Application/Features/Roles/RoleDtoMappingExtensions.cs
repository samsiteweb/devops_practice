using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles;

public static class RoleDtoMappingExtensions
{
    public static RoleDto MapToRoleDto(this Role projectFrom, IMapper mapper)
        => mapper.Map<RoleDto>(projectFrom);

    public static List<RoleDto> MapToRoleDtoList(this IEnumerable<Role> projectFrom, IMapper mapper)
        => projectFrom.Select(x => x.MapToRoleDto(mapper)).ToList();
}