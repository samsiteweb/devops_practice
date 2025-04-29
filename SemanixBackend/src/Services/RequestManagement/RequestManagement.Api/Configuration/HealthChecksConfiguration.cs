using HealthChecks.UI.Client;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Threading.Tasks;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.HealthChecks.HealthChecksConfiguration", Version = "1.0")]

namespace RequestManagement.Api.Configuration;

public static class HealthChecksConfiguration
{
    public static IServiceCollection ConfigureHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        // Add a custom check that only validates Dapr if in Kubernetes
        hcBuilder.AddCheck("dapr-sidecar", new EnvironmentAwareDaprHealthCheck(), 
            tags: new[] { "dapr" });

        return services;
    }

    public static IEndpointRouteBuilder MapDefaultHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
        // Additional health endpoint for Kubernetes probes
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            // Skip Dapr check in local environment
            Predicate = check => !IsLocalEnvironment() || !check.Tags.Contains("dapr"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
        return endpoints;
    }
    
    private static bool IsLocalEnvironment()
    {
        // Check if running locally (not in Kubernetes)
        // KUBERNETES_SERVICE_HOST is set in Kubernetes environments
        return string.IsNullOrEmpty(Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST"));
    }
}

// Custom health check that is environment-aware
public class EnvironmentAwareDaprHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // Check if we're running in Kubernetes
        var isKubernetes = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST"));
        
        if (!isKubernetes)
        {
            // If not in Kubernetes, always return healthy
            return Task.FromResult(HealthCheckResult.Healthy("Dapr check skipped in non-Kubernetes environment"));
        }
        
        // In Kubernetes - perform actual Dapr check
        try
        {
            var daprProcess = System.Diagnostics.Process.GetProcessesByName("daprd").FirstOrDefault();
            if (daprProcess != null && !daprProcess.HasExited)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Dapr sidecar is running"));
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("Dapr process 'daprd' not available or stopped"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"Dapr health check failed: {ex.Message}"));
        }
    }
}