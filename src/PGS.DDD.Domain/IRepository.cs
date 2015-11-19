namespace PGS.DDD.Domain
{
    public interface IRepository<T, TId> where T : IAggregateRoot, IEntity<TId>
    {
        void Save(T aggregate);

        T Get(TId id);
    }
}