using System.Collections.Generic;
using PGS.DDD.Domain;

namespace PGS.DDD.Application
{
    public interface IServiceBus
    {
        void Publish(IReadOnlyCollection<DomainEvent> messages);
    }
}
