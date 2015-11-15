using System.Collections.Generic;
using ImpromptuInterface;

namespace PGS.DDD.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        private readonly Queue<DomainEvent> _changes = new Queue<DomainEvent>();
        private int _version;

        IEnumerable<DomainEvent> IAggregateRoot.Changes => _changes;

        int IAggregateRoot.Version => _version;

        protected AggregateRoot(TId id) : base(id)
        {
        }

        protected void Handle(DomainEvent domainEvent)
        {
            Apply(domainEvent);
            Append(domainEvent);
        }

        private void Append(DomainEvent domainEvent)
        {
            _changes.Enqueue(domainEvent);
        }

        private void Apply(DomainEvent domainEvent)
        {
            var applyMethodName = "On" + domainEvent.GetType().Name;
            Impromptu.InvokeMemberAction(this, applyMethodName, domainEvent);
            _version++;
        }

        void IAggregateRoot.ReplayChanges(IEnumerable<DomainEvent> pastEvents)
        {
            foreach (var pastEvent in pastEvents)
            {
                Apply(pastEvent);
            }
        }
    }
}