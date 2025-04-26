using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using AccessManagement.Infrastructure.Persistence;
using Dapr.Client;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.StateManagement.DaprStateStoreRepository", Version = "1.0")]

namespace AccessManagement.Infrastructure.Repositories;

public class RoleDaprStateStoreRepository : DaprStateStoreRepositoryBase<Role>, IRoleRepository
{
    public RoleDaprStateStoreRepository(DaprClient daprClient, DaprStateStoreUnitOfWork unitOfWork)
         : base(daprClient: daprClient, unitOfWork: unitOfWork, enableTransactions: false, storeName: "statestore")
    {
    }

    public void Add(Role entity)
    {
        Upsert(entity.Id.ToString(), entity);
    }

    public void Update(Role entity)
    {
        Upsert(entity.Id.ToString(), entity);
    }

    public void Remove(Role entity)
    {
        Remove(entity.Id.ToString(), entity);
    }

    public Task<Role?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return FindByKeyAsync(id.ToString(), cancellationToken);
    }

    public Task<List<Role>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return FindByKeysAsync(ids.Select(id => id.ToString()).ToArray(), cancellationToken);
    }
}