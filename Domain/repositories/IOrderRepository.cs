namespace PointOfSale.Domain
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        bool Exists(long orderId);
        Order FindOrder(long orderId);
        Order UpdateOrder(Order order);
    }
}
