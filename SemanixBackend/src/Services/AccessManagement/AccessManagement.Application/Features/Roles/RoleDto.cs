using AccessManagement.Application.Common.Mappings;
using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles;

public record RoleDto : IMapFrom<Role>
{
    public RoleDto()
    {
        RoleName = null!;
        RoleCode = null!;
        RoleDescription = null!;
    }

    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string RoleCode { get; set; }
    public string RoleDescription { get; set; }

    public static RoleDto Create(Guid id, string roleName, string roleCode, string roleDescription)
    {
        return new RoleDto
        {
            Id = id,
            RoleName = roleName,
            RoleCode = roleCode,
            RoleDescription = roleDescription
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleDto>();
    }
}