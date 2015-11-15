namespace PGS.DDD.Domain
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}