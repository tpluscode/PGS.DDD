namespace PGS.DDD.Application
{
    public interface IServiceBus : IEventPublisher, IEventHandler
    {
    }
}
