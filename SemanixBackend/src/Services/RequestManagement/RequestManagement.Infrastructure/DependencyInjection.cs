using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestManagement.Application.Common.Eventing;
using RequestManagement.Application.Common.Interfaces;
using RequestManagement.Domain.Common.Interfaces;
using RequestManagement.Domain.Repositories;
using RequestManagement.Infrastructure.Configuration;
using RequestManagement.Infrastructure.Eventing;
using RequestManagement.Infrastructure.Persistence;
using RequestManagement.Infrastructure.Repositories;
using RequestManagement.Infrastructure.Services;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace RequestManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DaprStateStoreUnitOfWork>();
        services.AddScoped<IDaprStateStoreUnitOfWork>(provider => provider.GetRequiredService<DaprStateStoreUnitOfWork>());
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddScoped<IEventBus, EventBusImplementation>();
        services.AddScoped<IDaprStateStoreGenericRepository, DaprStateStoreGenericRepository>();
        services.AddHttpClients(configuration);
        return services;
    }
}