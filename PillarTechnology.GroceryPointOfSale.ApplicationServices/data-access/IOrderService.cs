namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IOrderService
    {
        OrderDto FindOrder(long orderId);
    }
}
