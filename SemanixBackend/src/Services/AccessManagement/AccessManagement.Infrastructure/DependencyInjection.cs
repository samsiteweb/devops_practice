using AccessManagement.Application.Common.Eventing;
using AccessManagement.Application.Common.Interfaces;
using AccessManagement.Domain.Common.Interfaces;
using AccessManagement.Domain.Repositories;
using AccessManagement.Infrastructure.Configuration;
using AccessManagement.Infrastructure.Eventing;
using AccessManagement.Infrastructure.Persistence;
using AccessManagement.Infrastructure.Repositories;
using AccessManagement.Infrastructure.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace AccessManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPermissionRepository, PermissionDaprStateStoreRepository>();
        services.AddScoped<IRoleRepository, RoleDaprStateStoreRepository>();
        services.AddScoped<IUserRepository, UserDaprStateStoreRepository>();
        services.AddScoped<IUserGroupsRepository, UserGroupsDaprStateStoreRepository>();
        services.AddScoped<DaprStateStoreUnitOfWork>();
        services.AddScoped<IDaprStateStoreUnitOfWork>(provider => provider.GetRequiredService<DaprStateStoreUnitOfWork>());
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddScoped<IEventBus, EventBusImplementation>();
        services.AddScoped<IDaprStateStoreGenericRepository, DaprStateStoreGenericRepository>();
        services.AddHttpClients(configuration);
        return services;
    }
}