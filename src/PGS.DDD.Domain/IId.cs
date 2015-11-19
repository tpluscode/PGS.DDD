namespace PGS.DDD.Domain
{
    public interface IId
    {
        // todo: consider Uri, or convert to Uri elsewhere
        string ToUniqueString();
    }
}