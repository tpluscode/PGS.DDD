namespace PGS.DDD.Application
{
    public interface IIdConverter<TId>
    {
        string FromId(TId id);
    }
}