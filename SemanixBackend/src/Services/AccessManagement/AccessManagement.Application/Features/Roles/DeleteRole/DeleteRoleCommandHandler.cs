using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.DeleteRole;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    [IntentManaged(Mode.Merge)]
    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.FindByIdAsync(request.Id, cancellationToken);
        if (existingRole is null)
        {
            throw new NotFoundException($"Could not find Role '{request.Id}'");
        }

        _roleRepository.Remove(existingRole);
    }
}