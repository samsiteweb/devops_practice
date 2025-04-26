using System.Collections.Concurrent;
using AccessManagement.Application.Common.Interfaces;
using AccessManagement.Domain.Common;
using AccessManagement.Domain.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.StateManagement.DaprStateStoreUnitOfWork", Version = "1.0")]

namespace AccessManagement.Infrastructure.Persistence;

public class DaprStateStoreUnitOfWork : IDaprStateStoreUnitOfWork
{
    private readonly ConcurrentQueue<Func<CancellationToken, Task>> _actions = new();
    private readonly ConcurrentDictionary<object, byte> _trackedEntities = new();
    private readonly IDomainEventService _domainEventService;

    public DaprStateStoreUnitOfWork(IDomainEventService domainEventService)
    {
        _domainEventService = domainEventService;
    }

    public void Track(object? entity)
    {
        if (entity is null)
        {
            return;
        }
        _trackedEntities.TryAdd(entity, default);
    }

    public void Enqueue(Func<CancellationToken, Task> action)
    {
        _actions.Enqueue(action);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchEvents(cancellationToken);
        while (_actions.TryDequeue(out var action))
        {
            await action(cancellationToken);
        }
    }

    private async Task DispatchEvents(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            var domainEventEntity = _trackedEntities
                .Keys
                .OfType<IHasDomainEvent>()
                .SelectMany(x => x.DomainEvents)
                .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

            if (domainEventEntity is null)
            {
                break;
            }

            domainEventEntity.IsPublished = true;
            await _domainEventService.Publish(domainEventEntity, cancellationToken);
        }
    }
}