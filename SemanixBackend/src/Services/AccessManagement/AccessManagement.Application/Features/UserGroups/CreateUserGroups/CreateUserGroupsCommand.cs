using AccessManagement.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AccessManagement.Application.Features.UserGroups.CreateUserGroups;

public class CreateUserGroupsCommand : IRequest<Guid>, ICommand
{
    public CreateUserGroupsCommand(string groupName, string groupCode, string groupDescription)
    {
        GroupName = groupName;
        GroupCode = groupCode;
        GroupDescription = groupDescription;
    }

    public string GroupName { get; set; }
    public string GroupCode { get; set; }
    public string GroupDescription { get; set; }
}