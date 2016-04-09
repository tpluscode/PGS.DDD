using System;
using FakeItEasy;
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
            var readModel1 = A.Fake<IReadModelBuilderFactory<TestBuilder>>();
            var readModel2 = A.Fake<IReadModelBuilderFactory<TestBuilder>>();

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
            var readModelBuilder = A.Fake<TestBuilder>();
            var readModelBuilderFactory = new TestFactory(readModelBuilder);
            eventHandler.AttachReadModelHandlers(readModelBuilderFactory);

            // when
            eventHandler.Handle(testEvent);

            // then
            A.CallTo(() => readModelBuilder.ApplyEvent(testEvent)).MustHaveHappened();
            A.CallTo(() => readModelBuilder.Save()).MustHaveHappened();
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

        public class TestFactory : IReadModelBuilderFactory<TestBuilder>
        {
            private readonly TestBuilder _readModelBuilder;

            public TestFactory(TestBuilder readModelBuilder)
            {
                _readModelBuilder = readModelBuilder;
            }

            public TestBuilder Create()
            {
                return _readModelBuilder;
            }
        }

        public abstract class TestBuilder : IReadModelBuilder<TestEvent>, IDisposable
        {
            public abstract void ApplyEvent(TestEvent domainEvent);
            public abstract void Clear();
            public abstract void Save();
            public abstract void Dispose();
        }
    }
}