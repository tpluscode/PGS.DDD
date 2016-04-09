namespace PGS.DDD.ReadModel
{
    public interface IReadModelBuilderFactory<out TBuilder> : IReadModelBuilderFactory where TBuilder : IReadModelBuilder
    {
        TBuilder Create();
    }
}