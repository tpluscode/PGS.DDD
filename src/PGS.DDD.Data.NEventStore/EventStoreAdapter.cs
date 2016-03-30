using System;
using System.Collections.Generic;
using System.Linq;
using NEventStore;
using PGS.DDD.Data.EventSourced;
using PGS.DDD.Domain;

namespace PGS.DDD.Data.NEventStore
{
    public class EventStoreAdapter : IEventStore
    {
        private readonly IStoreEvents _eventStore;

        public EventStoreAdapter(IStoreEvents eventStore)
        {
            _eventStore = eventStore;
        }

        public IEnumerable<DomainEvent> GetEvents(string id)
        {
            return _eventStore.OpenStream(id).CommittedEvents.Select(msg => msg.Body).Cast<DomainEvent>();
        }

        public void AppendEvents(string id, IEnumerable<DomainEvent> changes)
        {
            var eventStream = _eventStore.OpenStream(id);

            foreach (var domainEvent in changes)
            {
                eventStream.Add(new EventMessage
                {
                    Body = domainEvent
                });
            }

            eventStream.CommitChanges(Guid.NewGuid());
        }
    }
}
