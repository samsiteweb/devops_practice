using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.GetPermissionById;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    [IntentManaged(Mode.Merge)]
    public GetPermissionByIdQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.FindByIdAsync(request.Id, cancellationToken);
        if (permission is null)
        {
            throw new NotFoundException($"Could not find Permission '{request.Id}'");
        }

        return permission.MapToPermissionDto(_mapper);
    }
}