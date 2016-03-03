using System.Collections.Generic;
using PGS.DDD.Domain;

namespace PGS.Wykop.Data.EventSourced
{
    public interface IEventStore
    {
        IEnumerable<DomainEvent> GetEvents(string id);

        void AppendEvents(string id, IEnumerable<DomainEvent> changes);
    }
}