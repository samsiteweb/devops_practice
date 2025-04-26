using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.UpdateRole;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    [IntentManaged(Mode.Merge)]
    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.FindByIdAsync(request.Id, cancellationToken);
        if (existingRole is null)
        {
            throw new NotFoundException($"Could not find Role '{request.Id}'");
        }

        existingRole.RoleName = request.RoleName;
        existingRole.RoleCode = request.RoleCode;
        existingRole.RoleDescription = request.RoleDescription;

        _roleRepository.Update(existingRole);
    }
}