using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.UpdateUserGroups;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class UpdateUserGroupsCommandHandler : IRequestHandler<UpdateUserGroupsCommand>
{
    private readonly IUserGroupsRepository _userGroupsRepository;

    [IntentManaged(Mode.Merge)]
    public UpdateUserGroupsCommandHandler(IUserGroupsRepository userGroupsRepository)
    {
        _userGroupsRepository = userGroupsRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task Handle(UpdateUserGroupsCommand request, CancellationToken cancellationToken)
    {
        var existingUserGroups = await _userGroupsRepository.FindByIdAsync(request.Id, cancellationToken);
        if (existingUserGroups is null)
        {
            throw new NotFoundException($"Could not find UserGroups '{request.Id}'");
        }

        existingUserGroups.GroupName = request.GroupName;
        existingUserGroups.GroupCode = request.GroupCode;
        existingUserGroups.GroupDescription = request.GroupDescription;

        _userGroupsRepository.Update(existingUserGroups);
    }
}