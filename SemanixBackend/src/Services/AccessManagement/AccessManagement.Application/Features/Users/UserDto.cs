using AccessManagement.Application.Common.Mappings;
using AccessManagement.Domain;
using AccessManagement.Domain.Entities;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace AccessManagement.Application.Features.Users;

public record UserDto : IMapFrom<User>
{
    public UserDto()
    {
        FirstName = null!;
        LastName = null!;
        EmailAddress = null!;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public Gender Gender { get; set; }

    public static UserDto Create(Guid id, string firstName, string lastName, string emailAddress, Gender gender)
    {
        return new UserDto
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            EmailAddress = emailAddress,
            Gender = gender
        };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>();
    }
}