using System.Text.Json.Serialization;
using Dapr.Client;
using Intent.RoslynWeaver.Attributes;
using RequestManagement.Api.Configuration;
using RequestManagement.Api.Filters;
using RequestManagement.Application;
using RequestManagement.Infrastructure;
using Serilog;
using Serilog.Events;
using System.Threading;
using System.Threading.Tasks;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Program", Version = "1.0")]

namespace RequestManagement.Api;

public class Program
{
    //test build update 8
    public static void Main(string[] args)
    {
        using var logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services));
            
            // Check if Dapr is enabled
            var isDaprEnabledInConfig = builder.Configuration.GetValue<bool>("Dapr:Enabled", false);
            var isKubernetes = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST"));
            var shouldEnableDaprSidekick = isDaprEnabledInConfig && !isKubernetes;
            
            // Only add Dapr Sidekick if we're not running in Kubernetes
            if (shouldEnableDaprSidekick)
            {
                logger.Write(LogEventLevel.Information, "Enabling Dapr Sidekick for local development");
                builder.Services.AddDaprSidekick(builder.Configuration);
            }

            // Always register the DaprClient so that services requiring it don't fail
            // If Dapr is disabled in config, we'll use a mock implementation
            if (isDaprEnabledInConfig)
            {
                logger.Write(LogEventLevel.Information, "Registering real Dapr client");
                builder.Services.AddDaprClient();
                builder.Services.AddControllers().AddDapr();
            }
            else
            {
                logger.Write(LogEventLevel.Information, "Registering mock Dapr client");
                builder.Services.AddSingleton<DaprClient>(new MockDaprClient());
                
                // Log a warning that Dapr features won't work
                logger.Write(LogEventLevel.Warning, "Dapr is disabled. Any features requiring Dapr will not function.");
            }

            builder.Services.AddControllers(
                opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.ConfigureApplicationSecurity(builder.Configuration);
            builder.Services.ConfigureHealthChecks(builder.Configuration);
            builder.Services.ConfigureProblemDetails();
            builder.Services.ConfigureApiVersioning();
            builder.Services.ConfigureBugsnag(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.ConfigureSwagger(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSerilogRequestLogging();
            app.UseExceptionHandler();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapDefaultHealthChecks();
            app.MapControllers();
            app.UseSwashbuckle(builder.Configuration);

            logger.Write(LogEventLevel.Information, "Starting web host");

            app.Run();
        }
        catch (HostAbortedException)
        {
            // Excluding HostAbortedException from being logged, as this is an expected
            // exception when working with EF Core migrations (as per the .NET team on the below link)
            // https://github.com/dotnet/efcore/issues/29809#issuecomment-1344101370
        }
        catch (Exception ex)
        {
            logger.Write(LogEventLevel.Fatal, ex, "Unhandled exception");
        }
    }
}

// Stub implementation of DaprClient that doesn't do anything but prevents dependency injection errors
public class MockDaprClient : DaprClient
{
    // Common operations that might be used by your application
    public override Task<T> GetStateAsync<T>(string storeName, string key, ConsistencyMode? consistencyMode = null, 
        IReadOnlyDictionary<string, string> metadata = null, CancellationToken cancellationToken = default)
    {
        // Return default value for the type
        return Task.FromResult<T>(default);
    }

    public override Task SaveStateAsync<T>(string storeName, string key, T value, 
        StateOptions stateOptions = null, IReadOnlyDictionary<string, string> metadata = null, 
        CancellationToken cancellationToken = default)
    {
        // Do nothing, just return completed task
        return Task.CompletedTask;
    }

    public override Task PublishEventAsync<T>(string pubsubName, string topicName, T data, 
        IReadOnlyDictionary<string, string> metadata = null, CancellationToken cancellationToken = default)
    {
        // Do nothing, just return completed task
        return Task.CompletedTask;
    }

    public override ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}