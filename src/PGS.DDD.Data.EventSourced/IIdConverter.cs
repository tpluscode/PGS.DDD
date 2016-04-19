namespace PGS.DDD.Data.EventSourced
{
    public interface IIdConverter<TId>
    {
        string FromId(TId id);
    }
}