using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace AccessManagement.Application.Features.Users;

public static class UserDtoMappingExtensions
{
    public static UserDto MapToUserDto(this User projectFrom, IMapper mapper)
        => mapper.Map<UserDto>(projectFrom);

    public static List<UserDto> MapToUserDtoList(this IEnumerable<User> projectFrom, IMapper mapper)
        => projectFrom.Select(x => x.MapToUserDto(mapper)).ToList();
}