using PGS.DDD.Domain;

namespace PGS.DDD.ReadModel
{
    public interface IReadModel<in TDomainEvent> : IReadModel where TDomainEvent : DomainEvent
    {
        void UpdateReadModel(TDomainEvent domainEvent);
    }
}
