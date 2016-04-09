using PGS.DDD.Domain;

namespace PGS.DDD.ReadModel
{
    public interface IReadModelBuilder<in TDomainEvent> : IReadModelBuilder where TDomainEvent : DomainEvent
    {
        void ApplyEvent(TDomainEvent domainEvent);
    }
}
