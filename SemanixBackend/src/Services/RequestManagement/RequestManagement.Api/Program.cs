using System.Text.Json.Serialization;
using Intent.RoslynWeaver.Attributes;
using RequestManagement.Api.Configuration;
using RequestManagement.Api.Filters;
using RequestManagement.Application;
using RequestManagement.Infrastructure;
using Serilog;
using Serilog.Events;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Program", Version = "1.0")]

namespace RequestManagement.Api;

public class Program
{
    //test build update 7
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
            builder.Services.AddDaprSidekick(builder.Configuration);

            builder.Services.AddControllers(
                opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            .AddDapr();
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