namespace PointOfSale.Domain
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        Order FindOrder(long id);
        Order UpdateOrder(Order order);
    }
}