using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions;

public static class PermissionDtoMappingExtensions
{
    public static PermissionDto MapToPermissionDto(this Permission projectFrom, IMapper mapper)
        => mapper.Map<PermissionDto>(projectFrom);

    public static List<PermissionDto> MapToPermissionDtoList(this IEnumerable<Permission> projectFrom, IMapper mapper)
        => projectFrom.Select(x => x.MapToPermissionDto(mapper)).ToList();
}