using System;
using System.Linq;
using PGS.DDD.Domain;

namespace PGS.DDD.Data.EventSourced
{
    public class Repository<T, TId> : IRepository<T, TId> where T : IEventSourcedAggregateRoot, IEntity<TId>
    {
        private readonly IEventStore _eventStore;
        private readonly IIdConverter<TId> _converter;

        public Repository(IEventStore eventStore, IIdConverter<TId> converter)
        {
            _eventStore = eventStore;
            _converter = converter;
        }

        public void Save(T aggregate)
        {
            string id = _converter.FromId(aggregate.Id);
            _eventStore.AppendEvents(id, aggregate.Changes);
        }

        public T Get(TId id)
        {
            string idString = _converter.FromId(id);
            var events = _eventStore.GetEvents(idString);
            T aggregate = (T) Activator.CreateInstance(typeof (T), id);
            aggregate.ReplayChanges(events.ToArray());
            return aggregate;
        }
    }
}
