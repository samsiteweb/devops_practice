using Intent.RoslynWeaver.Attributes;
using RequestManagement.Domain.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.StateManagement.DaprStateStoreRepositoryInterface", Version = "1.0")]

namespace RequestManagement.Domain.Repositories;

public interface IDaprStateStoreRepository<TDomain> : IRepository<TDomain>
{
    IDaprStateStoreUnitOfWork UnitOfWork { get; }
    Task<List<TDomain>> FindAllAsync(CancellationToken cancellationToken = default);
}