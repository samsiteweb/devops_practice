using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using AccessManagement.Infrastructure.Persistence;
using Dapr.Client;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.StateManagement.DaprStateStoreRepository", Version = "1.0")]

namespace AccessManagement.Infrastructure.Repositories;

public class UserDaprStateStoreRepository : DaprStateStoreRepositoryBase<User>, IUserRepository
{
    public UserDaprStateStoreRepository(DaprClient daprClient, DaprStateStoreUnitOfWork unitOfWork)
         : base(daprClient: daprClient, unitOfWork: unitOfWork, enableTransactions: false, storeName: "statestore")
    {
    }

    public void Add(User entity)
    {
        Upsert(entity.Id.ToString(), entity);
    }

    public void Update(User entity)
    {
        Upsert(entity.Id.ToString(), entity);
    }

    public void Remove(User entity)
    {
        Remove(entity.Id.ToString(), entity);
    }

    public Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return FindByKeyAsync(id.ToString(), cancellationToken);
    }

    public Task<List<User>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return FindByKeysAsync(ids.Select(id => id.ToString()).ToArray(), cancellationToken);
    }
}