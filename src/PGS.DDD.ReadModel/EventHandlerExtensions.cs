using System;
using System.Collections.Generic;
using System.Linq;
using ImpromptuInterface;
using ImpromptuInterface.InvokeExt;
using PGS.DDD.Domain;
using PGS.DDD.Eventing;

namespace PGS.DDD.ReadModel
{
    public static class EventHandlerExtensions
    {
        public static void AttachReadModelHandlers(this IEventHandler eventHandler, params IReadModelBuilderFactory[] builderFactories)
        {
            foreach (var factory in builderFactories)
            {
                var handledTypes =
                    from factoryInterface in factory.GetType().GetInterfaces(typeof(IReadModelBuilderFactory<>))
                    let builderType = factoryInterface.GenericTypeArguments.Single()
                    from handlerInterface in builderType.GetInterfaces(typeof(IReadModelBuilder<>))
                    let eventType = handlerInterface.GenericTypeArguments.Single()
                    select new[] { builderType, eventType };

                var handleFunc = InvokeMemberName.Create;
                foreach (var typePair in handledTypes)
                {
                    var handlerFuncName = handleFunc("Handle", typePair);
                    var handler = Impromptu.InvokeMember(typeof(EventHandlerExtensions).WithStaticContext(), handlerFuncName, factory);

                    var busHandlerFuncName = handleFunc("Handle", new[] { typePair[1] });
                    Impromptu.InvokeMemberAction(eventHandler, busHandlerFuncName, handler);
                }
            }
        }

        private static IEnumerable<Type> GetInterfaces(this Type type, Type interfaceType)
        {
            return from factoryInterface in type.GetInterfaces()
                where factoryInterface.IsConstructedGenericType
                where factoryInterface.GetGenericTypeDefinition() == interfaceType
                select factoryInterface;
        }

        private static Action<TEvent> Handle<TBuilder, TEvent>(IReadModelBuilderFactory<TBuilder> factory)
            where TEvent : DomainEvent
            where TBuilder : IReadModelBuilder<TEvent>, IDisposable
        {
            return ev =>
            {
                using (var readModel = factory.Create())
                {
                    readModel.ApplyEvent(ev);
                    readModel.Save();
                }
            };
        }
    }
}