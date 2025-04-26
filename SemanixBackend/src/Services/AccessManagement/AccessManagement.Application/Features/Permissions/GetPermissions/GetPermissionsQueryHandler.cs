using AccessManagement.Domain.Repositories;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace AccessManagement.Application.Features.Permissions.GetPermissions;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    [IntentManaged(Mode.Merge)]
    public GetPermissionsQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionRepository.FindAllAsync(cancellationToken);
        return permissions.MapToPermissionDtoList(_mapper);
    }
}