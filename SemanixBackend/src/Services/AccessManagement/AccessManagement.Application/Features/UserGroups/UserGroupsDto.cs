using AccessManagement.Application.Common.Mappings;
using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups;

public record UserGroupsDto : IMapFrom<Domain.Entities.UserGroups>
{
    public UserGroupsDto()
    {
        GroupName = null!;
        GroupCode = null!;
        GroupDescription = null!;
    }

    public Guid Id { get; set; }
    public string GroupName { get; set; }
    public string GroupCode { get; set; }
    public string GroupDescription { get; set; }

    public static UserGroupsDto Create(Guid id, string groupName, string groupCode, string groupDescription)
    {
        return new UserGroupsDto
        {
            Id = id,
            GroupName = groupName,
            GroupCode = groupCode,
            GroupDescription = groupDescription
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.UserGroups, UserGroupsDto>();
    }
}