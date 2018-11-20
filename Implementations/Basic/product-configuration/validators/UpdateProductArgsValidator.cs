namespace PointOfSale.Implementations.Basic
{
    public class UpdateProductArgsValidator : UpsertProductArgsValidator
    {
        public UpdateProductArgsValidator(
            UpdateProductNameValidator updateProductNameValidator,
            SellByTypeValidator sellByTypeValidator,
            RetailPriceValidator retailPriceValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        ) : base(
            sellByTypeValidator,
            retailPriceValidator,
            iUpsertMassProductArgsValidator
        )
        {
            Include(updateProductNameValidator);
        }
    }
}
