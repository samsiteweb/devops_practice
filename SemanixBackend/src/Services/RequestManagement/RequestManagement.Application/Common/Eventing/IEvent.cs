using Intent.RoslynWeaver.Attributes;
using MediatR;
using RequestManagement.Application.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Dapr.AspNetCore.Pubsub.EventInterface", Version = "1.0")]

namespace RequestManagement.Application.Common.Eventing;

public interface IEvent : IRequest, ICommand
{
    string PubsubName { get; }
    string TopicName { get; }
}