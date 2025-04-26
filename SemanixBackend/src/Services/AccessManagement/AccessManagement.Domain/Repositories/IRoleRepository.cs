using AccessManagement.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace AccessManagement.Domain.Repositories;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public interface IRoleRepository : IDaprStateStoreRepository<Role>
{
    [IntentManaged(Mode.Fully)]
    Task<Role?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    [IntentManaged(Mode.Fully)]
    Task<List<Role>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
}