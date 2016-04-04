using System.Collections.Generic;
using PGS.DDD.Domain;

namespace PGS.DDD.Data.EventSourced
{
    public interface IEventStore
    {
        /// <summary>
        /// Gets all events from the dawn of time for a stream.
        /// </summary>
        IEnumerable<DomainEvent> GetEvents(string id);

        /// <summary>
        /// Gets all events from the dawn of time.
        /// </summary>
        IEnumerable<DomainEvent> GetEvents();

        void AppendEvents(string id, IEnumerable<DomainEvent> changes);
    }
}