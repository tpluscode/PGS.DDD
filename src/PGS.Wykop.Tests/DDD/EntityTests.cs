using FluentAssertions;
using NUnit.Framework;
using PGS.DDD.Domain;

namespace PGS.Wykop.Tests.DDD
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Entity_should_be_constructed_with_identiy()
        {
            // when
            var entity = new TestEntity(10);

            // then
            entity.Id.Should().Be(10);
        }

        [Test]
        public void Entities_with_same_Id_should_be_equal()
        {
            // when
            var entity = new TestEntity(10);
            var entity2 = new TestEntity(10);

            // then
            Assert.That(entity == entity2);
            Assert.That(entity.Equals(entity2));
        }

        [Test]
        public void Entities_with_different_Id_should_not_be_equal()
        {
            // when
            var entity = new TestEntity(10);
            var entity2 = new TestEntity(11);

            // then
            Assert.That(entity == entity2, Is.False);
            Assert.That(entity.Equals(entity2), Is.False);
        }

        public class TestEntity : Entity<int>
        {
            public TestEntity(int id) : base(id)
            {
            }
        }
    }
}