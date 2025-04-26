using AccessManagement.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace AccessManagement.Domain.Repositories;

[IntentManaged(Mode.Merge, Signature = Mode.Fully)]
public interface IUserGroupsRepository : IDaprStateStoreRepository<UserGroups>
{
    [IntentManaged(Mode.Fully)]
    Task<UserGroups?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    [IntentManaged(Mode.Fully)]
    Task<List<UserGroups>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
}