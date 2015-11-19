using System.Collections.Generic;
using PGS.DDD.Domain;

namespace PGS.DDD.Application
{
    public interface IEventPublisher
    {
        void Publish(IReadOnlyCollection<DomainEvent> messages);
    }
}