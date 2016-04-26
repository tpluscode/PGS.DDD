using System.Collections.Generic;
using PGS.DDD.Domain;

namespace PGS.DDD.Data.EventSourced
{
    public interface IEventSourcedAggregateRoot : IAggregateRoot
    {
        void ReplayChanges(IEnumerable<DomainEvent> pastEvents);
    }
}