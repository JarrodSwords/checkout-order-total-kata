namespace PointOfSale.Domain
{
    public interface IFactory<T>
    {
        T Create();
    }
}
