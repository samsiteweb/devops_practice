using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.UserGroups.CreateUserGroups;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class CreateUserGroupsCommandHandler : IRequestHandler<CreateUserGroupsCommand, Guid>
{
    private readonly IUserGroupsRepository _userGroupsRepository;

    [IntentManaged(Mode.Merge)]
    public CreateUserGroupsCommandHandler(IUserGroupsRepository userGroupsRepository)
    {
        _userGroupsRepository = userGroupsRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<Guid> Handle(CreateUserGroupsCommand request, CancellationToken cancellationToken)
    {
        var newUserGroups = new Domain.Entities.UserGroups
        {
            GroupName = request.GroupName,
            GroupCode = request.GroupCode,
            GroupDescription = request.GroupDescription,
        };

        _userGroupsRepository.Add(newUserGroups);
        await _userGroupsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return newUserGroups.Id;
    }
}