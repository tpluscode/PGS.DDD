using System;
using System.Collections.Generic;
using PGS.DDD.Application;
using PGS.DDD.Domain;

namespace PGS.DDD.ServiceBus
{
    public class InMemoryBus : IServiceBus
    {
        private readonly IDictionary<Type, ICollection<Action<DomainEvent>>> _handlers;

        public InMemoryBus()
        {
            _handlers = new Dictionary<Type, ICollection<Action<DomainEvent>>>();
        }

        public void Publish(IReadOnlyCollection<DomainEvent> messages)
        {
            foreach (var message in messages)
            {
                if (!_handlers.ContainsKey(message.GetType()))
                {
                    continue;
                }

                foreach (var handler in _handlers[message.GetType()])
                {
                    handler(message);
                }
            }
        }

        public void Handle<T>(Action<T> handler) where T : DomainEvent
        {
            if (_handlers.ContainsKey(typeof(T)) == false)
            {
                _handlers[typeof(T)] = new List<Action<DomainEvent>>();
            }

            _handlers[typeof(T)].Add(message => handler((T)message));
        }
    }
}