using System.Collections.Generic;
using System.Linq;
using PGS.DDD.Domain;

namespace PGS.DDD.Data.EventSourced
{
    public class InMemoryEventStore : IEventStore
    {
        readonly IDictionary<string, Queue<DomainEvent>> _eventStreams = new Dictionary<string, Queue<DomainEvent>>();

        public IEnumerable<DomainEvent> GetEvents(string id)
        {
            if (_eventStreams.ContainsKey(id))
            {
                return _eventStreams[id];
            }

            return Enumerable.Empty<DomainEvent>();
        }

        public IEnumerable<DomainEvent> GetEvents()
        {
            return from eventStream in _eventStreams
                   from ev in eventStream.Value
                   orderby ev.Date
                   select ev;
        }

        public void AppendEvents(string id, IEnumerable<DomainEvent> changes)
        {
            if (_eventStreams.ContainsKey(id) == false)
            {
                _eventStreams[id] = new Queue<DomainEvent>();
            }

            foreach (var change in changes)
            {
                _eventStreams[id].Enqueue(change);
            }
        }
    }
}