using System.Collections.Generic;

namespace PGS.DDD.Domain
{
    public interface IAggregateRoot
    {
        int Version { get; }

        IEnumerable<DomainEvent> Changes { get; }
    }
}