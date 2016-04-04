using PGS.DDD.Eventing;

namespace PGS.DDD.ServiceBus
{
    public interface IServiceBus : IEventPublisher, IEventHandler
    {
    }
}
