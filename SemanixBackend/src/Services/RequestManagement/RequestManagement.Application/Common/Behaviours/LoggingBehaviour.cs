using Intent.RoslynWeaver.Attributes;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using RequestManagement.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.Behaviours.LoggingBehaviour", Version = "1.0")]

namespace RequestManagement.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest>> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId;
        var userName = _currentUserService.UserName;

        _logger.LogInformation("RequestManagement Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);
        return Task.CompletedTask;
    }
}