using FluentAssertions;
using PGS.DDD.Domain;
using Xunit;

namespace PGS.DDD.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Entity_should_be_constructed_with_identiy()
        {
            // when
            var entity = new TestEntity(10);

            // then
            entity.Id.Should().Be(10);
        }

        [Fact]
        public void Entities_with_same_Id_should_be_equal()
        {
            // when
            var entity = new TestEntity(10);
            var entity2 = new TestEntity(10);

            // then
            Assert.True(entity == entity2);
            Assert.True(entity.Equals(entity2));
        }

        [Fact]
        public void Entities_with_different_Id_should_not_be_equal()
        {
            // when
            var entity = new TestEntity(10);
            var entity2 = new TestEntity(11);

            // then
            Assert.False(entity == entity2);
            Assert.False(entity.Equals(entity2));
        }

        public class TestEntity : Entity<int>
        {
            public TestEntity(int id) : base(id)
            {
            }
        }
    }
}