namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public interface IScannable
    {
        int Id { get; set; }
        Product Product { get; }
    }
}