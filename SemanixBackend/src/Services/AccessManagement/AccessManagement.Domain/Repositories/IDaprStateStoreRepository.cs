using AccessManagement.Domain.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.StateManagement.DaprStateStoreRepositoryInterface", Version = "1.0")]

namespace AccessManagement.Domain.Repositories;

public interface IDaprStateStoreRepository<TDomain> : IRepository<TDomain>
{
    IDaprStateStoreUnitOfWork UnitOfWork { get; }
    Task<List<TDomain>> FindAllAsync(CancellationToken cancellationToken = default);
}