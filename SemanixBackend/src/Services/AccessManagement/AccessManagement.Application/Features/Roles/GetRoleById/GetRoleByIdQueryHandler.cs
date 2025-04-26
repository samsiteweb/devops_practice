using AccessManagement.Domain.Common.Exceptions;
using AccessManagement.Domain.Repositories;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace AccessManagement.Application.Features.Roles.GetRoleById;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    [IntentManaged(Mode.Merge)]
    public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    [IntentManaged(Mode.Fully, Body = Mode.Fully)]
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindByIdAsync(request.Id, cancellationToken);
        if (role is null)
        {
            throw new NotFoundException($"Could not find Role '{request.Id}'");
        }

        return role.MapToRoleDto(_mapper);
    }
}