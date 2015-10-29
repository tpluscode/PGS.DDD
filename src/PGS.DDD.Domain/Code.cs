using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpromptuInterface;

namespace PGS.DDD.Domain
{
    public class DomainEvent
    {
        public DateTime Date { get; private set; }

        public DomainEvent()
        {
            Date = DateTime.Now;
        }
    }

    public interface IAggregateRoot
    {
        int Version { get; }

        IEnumerable<DomainEvent> Changes { get; }

        void ReplayChanges(IEnumerable<DomainEvent> pastEvents);
    }

    public interface IEntity<out TId>
    {
        TId Id { get; }
    }

    public abstract class Entity<TId> : IEntity<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }

        protected bool Equals(Entity<TId> other)
        {
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity<TId>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }
    }

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
