using AccessManagement.Application.Common.Mappings;
using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions;

public record PermissionDto : IMapFrom<Permission>
{
    public PermissionDto()
    {
        PermissionName = null!;
        PermissionCode = null!;
        PermissionDescription = null!;
    }

    public Guid Id { get; set; }
    public string PermissionName { get; set; }
    public string PermissionCode { get; set; }
    public string PermissionDescription { get; set; }

    public static PermissionDto Create(Guid id, string permissionName, string permissionCode, string permissionDescription)
    {
        return new PermissionDto
        {
            Id = id,
            PermissionName = permissionName,
            PermissionCode = permissionCode,
            PermissionDescription = permissionDescription
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Permission, PermissionDto>();
    }
}