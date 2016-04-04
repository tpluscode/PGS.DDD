using System;
using System.Linq;
using ImpromptuInterface;
using ImpromptuInterface.InvokeExt;
using PGS.DDD.Domain;
using PGS.DDD.Eventing;

namespace PGS.DDD.ReadModel
{
    public static class EventHandlerExtensions
    {
        public static void AttachReadModelHandlers(this IEventHandler eventHandler, params IReadModel[] readModels)
        {
            foreach (var readModel in readModels)
            {
                var interfaces = from iface in readModel.GetType().GetInterfaces()
                    where iface.IsConstructedGenericType
                    where iface.GetGenericTypeDefinition() == typeof(IReadModel<>)
                    select iface;

                var handleFunc = InvokeMemberName.Create;
                foreach (var iface in interfaces)
                {
                    var handlerFuncName = handleFunc("Handle", iface.GetGenericArguments());
                    var handler = Impromptu.InvokeMember(typeof(EventHandlerExtensions).WithStaticContext(), handlerFuncName, readModel);

                    Impromptu.InvokeMemberAction(eventHandler, handlerFuncName, handler);
                }
            }
        }

        private static Action<TEvent> Handle<TEvent>(IReadModel<TEvent> readModel) where TEvent : DomainEvent
        {
            return ev =>
            {
                readModel.UpdateReadModel(ev);
                readModel.Save();
            };
        }
    }
}