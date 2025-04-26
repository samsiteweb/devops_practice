using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace AccessManagement.Application.Features.Permissions.UpdatePermission;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
{
    private readonly IPermissionRepository _permissionRepository;

    [IntentManaged(Mode.Merge)]
    public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var existingPermission = await _permissionRepository.FindByIdAsync(request.Id, cancellationToken);
        if (existingPermission is null)
        {
            throw new NotFoundException($"Could not find Permission '{request.Id}'");
        }

        existingPermission.PermissionName = request.PermissionName;
        existingPermission.PermissionCode = request.PermissionCode;
        existingPermission.PermissionDescription = request.PermissionDescription;

        _permissionRepository.Update(existingPermission);
    }
}