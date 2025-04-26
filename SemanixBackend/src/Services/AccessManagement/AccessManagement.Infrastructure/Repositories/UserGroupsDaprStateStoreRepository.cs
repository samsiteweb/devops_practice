using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Repositories;
using AccessManagement.Infrastructure.Persistence;
using Dapr.Client;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.StateManagement.DaprStateStoreRepository", Version = "1.0")]

namespace AccessManagement.Infrastructure.Repositories;

public class UserGroupsDaprStateStoreRepository : DaprStateStoreRepositoryBase<UserGroups>, IUserGroupsRepository
{
    public UserGroupsDaprStateStoreRepository(DaprClient daprClient, DaprStateStoreUnitOfWork unitOfWork)
         : base(daprClient: daprClient, unitOfWork: unitOfWork, enableTransactions: false, storeName: "statestore")
    {
    }

    public void Add(UserGroups entity)
    {
        Upsert(entity.Id.ToString(), entity);
    }

    public void Update(UserGroups entity)
    {
        Upsert(entity.Id.ToString(), entity);
    }

    public void Remove(UserGroups entity)
    {
        Remove(entity.Id.ToString(), entity);
    }

    public Task<UserGroups?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return FindByKeyAsync(id.ToString(), cancellationToken);
    }

    public Task<List<UserGroups>> FindByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return FindByKeysAsync(ids.Select(id => id.ToString()).ToArray(), cancellationToken);
    }
}