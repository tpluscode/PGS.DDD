using System;
using PGS.DDD.Domain;

namespace PGS.DDD.Application
{
    public interface IEventHandler
    {
        void Handle<T>(Action<T> handler) where T : DomainEvent;
    }
}