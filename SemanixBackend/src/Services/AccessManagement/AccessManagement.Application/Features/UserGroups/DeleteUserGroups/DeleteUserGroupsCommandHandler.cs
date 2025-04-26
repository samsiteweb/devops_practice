using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.DeleteUserGroups;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class DeleteUserGroupsCommandHandler : IRequestHandler<DeleteUserGroupsCommand>
{
    private readonly IUserGroupsRepository _userGroupsRepository;

    [IntentManaged(Mode.Merge)]
    public DeleteUserGroupsCommandHandler(IUserGroupsRepository userGroupsRepository)
    {
        _userGroupsRepository = userGroupsRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task Handle(DeleteUserGroupsCommand request, CancellationToken cancellationToken)
    {
        var existingUserGroups = await _userGroupsRepository.FindByIdAsync(request.Id, cancellationToken);
        if (existingUserGroups is null)
        {
            throw new NotFoundException($"Could not find UserGroups '{request.Id}'");
        }

        _userGroupsRepository.Remove(existingUserGroups);
    }
}