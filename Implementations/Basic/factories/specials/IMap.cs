namespace PointOfSale.Implementations
{
    public interface IMap<TSource, TResult>
    {
        TResult Map(TSource source);
    }
}
