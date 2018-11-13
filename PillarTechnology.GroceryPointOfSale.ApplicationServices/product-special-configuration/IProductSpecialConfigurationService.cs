namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductSpecialConfigurationService<T>
    {
        ProductDto CreateSpecial(T args);
    }
}
