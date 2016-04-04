using System;
using FakeItEasy;
using PGS.DDD.Application;
using PGS.DDD.Domain;
using PGS.DDD.Eventing;
using PGS.DDD.ReadModel;
using Xunit;

namespace PGS.DDD.Tests
{
    public class EventHandlerExtensionsTests
    {
        [Fact]
        public void Should_wireup_handlers_with_event_handler()
        {
            // given
            var eventHandler = A.Fake<IEventHandler>();
            var readModel1 = A.Fake<IReadModel<TestEvent>>();
            var readModel2 = A.Fake<IReadModel<TestEvent>>();

            // when
            eventHandler.AttachReadModelHandlers(readModel1, readModel2);

            // then
            A.CallTo(() => eventHandler.Handle(A<Action<TestEvent>>._)).MustHaveHappened(Repeated.Like(i => i == 2));
        }

        [Fact]
        public void Should_wireup_correct_handler()
        {
            // given
            var testEvent = new TestEvent();
            var eventHandler = new TestHandler();
            var readModel = A.Fake<IReadModel<TestEvent>>();
            eventHandler.AttachReadModelHandlers(readModel);

            // when
            eventHandler.Handle(testEvent);

            // then
            A.CallTo(() => readModel.UpdateReadModel(testEvent)).MustHaveHappened();
            A.CallTo(() => readModel.Save()).MustHaveHappened();
        }

        private class TestHandler : IEventHandler
        {
            private Action<TestEvent> _handler;

            public void Handle(TestEvent e)
            {
                _handler(e);
            }

            public void Handle<T>(Action<T> handler) where T : DomainEvent
            {
                _handler = (Action<TestEvent>) handler;
            }
        }

        public class TestEvent : DomainEvent
        {
        }
    }
}