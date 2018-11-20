namespace PointOfSale.Implementations.Basic
{
    public class UpdateProductArgsValidator : UpsertProductArgsValidator
    {
        public UpdateProductArgsValidator(
            ProductNameExistsValidator updateProductNameValidator,
            SellByTypeValidator sellByTypeValidator,
            IUpsertEachesProductArgsValidator retailPriceValidator,
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
