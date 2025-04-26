using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.UpdateUserGroups;

public class UpdateUserGroupsCommand : IRequest, ICommand
{
    public UpdateUserGroupsCommand(string groupName, string groupCode, string groupDescription, Guid id)
    {
        GroupName = groupName;
        GroupCode = groupCode;
        GroupDescription = groupDescription;
        Id = id;
    }

    public string GroupName { get; set; }
    public string GroupCode { get; set; }
    public string GroupDescription { get; set; }
    public Guid Id { get; set; }
}