using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.CreatePermission;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
{
    private readonly IPermissionRepository _permissionRepository;

    [IntentManaged(Mode.Merge)]
    public CreatePermissionCommandHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var newPermission = new Permission
        {
            PermissionName = request.PermissionName,
            PermissionCode = request.PermissionCode,
            PermissionDescription = request.PermissionDescription,
        };

        _permissionRepository.Add(newPermission);
        await _permissionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return newPermission.Id;
    }
}