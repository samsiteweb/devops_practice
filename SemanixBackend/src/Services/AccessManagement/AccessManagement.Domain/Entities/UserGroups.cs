using AccessManagement.Domain.Common;
using AccessManagement.Domain.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace AccessManagement.Domain.Entities;

public class UserGroups : IHasDomainEvent, IAuditable
{
    private Guid? _id;

    public UserGroups()
    {
        GroupName = null!;
        GroupCode = null!;
        GroupDescription = null!;
        CreatedBy = null!;
    }

    public Guid Id
    {
        get => _id ??= Guid.NewGuid();
        set => _id = value;
    }

    public string GroupName { get; set; }

    public string GroupCode { get; set; }

    public string GroupDescription { get; set; }

    public bool IsDeleted { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = [];

    void IAuditable.SetCreated(string createdBy, DateTimeOffset createdDate) => (CreatedBy, CreatedDate) = (createdBy, createdDate);

    void IAuditable.SetUpdated(string updatedBy, DateTimeOffset updatedDate) => (UpdatedBy, UpdatedDate) = (updatedBy, updatedDate);
}