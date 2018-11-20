namespace PointOfSale.Domain
{
    public interface IUpdate<T>
    {
        T Update(T source);
    }
}
