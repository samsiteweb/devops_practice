using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Roles.CreateRole;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;

    [IntentManaged(Mode.Merge)]
    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var newRole = new Role
        {
            RoleName = request.RoleName,
            RoleCode = request.RoleCode,
            RoleDescription = request.RoleDescription,
        };

        _roleRepository.Add(newRole);
        await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return newRole.Id;
    }
}