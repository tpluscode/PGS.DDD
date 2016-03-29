﻿using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PGS.DDD.Domain;

namespace PGS.Wykop.Tests.DDD
{
    [TestFixture]
    public class AggregateRootTests
    {
        [Test]
        public void Should_increment_version_when_events_are_applied()
        {
            // given
            var ar = new TestPersonAR(1);

            // when
            ar.SetName("Tom");
            ar.SetName("Frank");
            ar.SetName("George");

            // then
            IAggregateRoot baseRoot = ar;
            baseRoot.Version.Should().Be(3);
        }

        [Test]
        public void Applied_events_should_be_appended_to_changes()
        {
            // given
            var ar = new TestPersonAR(1);

            // when
            ar.SetName("Tom");
            ar.SetName("Frank");
            ar.SetName("George");

            // then
            IAggregateRoot baseRoot = ar;
            baseRoot.Changes.Should().HaveCount(3);
            baseRoot.Changes.Should().ContainItemsAssignableTo<TestPersonAR.NameChanged>();
        }

        [Test]
        public void Empty_aggregate_root_version_should_be_0()
        {
            // given
            var ar = new TestPersonAR(1);

            // then
            IAggregateRoot baseRoot = ar;
            baseRoot.Version.Should().Be(0);
        }

        [Test]
        public void Replaying_history_should_set_version()
        {
            // given
            var ar = new TestPersonAR(1);
            IAggregateRoot baseRoot = ar;

            // when
            baseRoot.ReplayChanges(History);

            // then
            baseRoot.Version.Should().Be(4);
        }

        [Test]
        public void Replaying_history_should_apply_values()
        {
            // given
            var ar = new TestPersonAR(1);
            IAggregateRoot baseRoot = ar;

            // when
            baseRoot.ReplayChanges(History);

            // then
            ar.Name.Should().Be("Frank");
            ar.Surname.Should().Be("Zappa");
        }

        [Test]
        public void Replaying_history_should_not_enqueue_changes()
        {
            // given
            IAggregateRoot baseRoot = new TestPersonAR(1);

            // when
            baseRoot.ReplayChanges(History);

            // then
            baseRoot.Changes.Should().BeEmpty();
        }

        private IEnumerable<DomainEvent> History => new DomainEvent[]
        {
            new TestPersonAR.NameChanged("Tom"),
            new TestPersonAR.NameChanged("John"),
            new TestPersonAR.NameChanged("Frank"),
            new TestPersonAR.SurnameChanged("Zappa")
        };
    }
}