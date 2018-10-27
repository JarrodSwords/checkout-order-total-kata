namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICreateProductValidator
    {
        void Validate(ProductDto productDto);
    }
}