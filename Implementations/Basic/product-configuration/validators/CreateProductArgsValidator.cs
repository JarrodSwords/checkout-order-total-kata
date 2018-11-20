namespace PointOfSale.Implementations.Basic
{
    public class CreateProductArgsValidator : UpsertProductArgsValidator
    {
        public CreateProductArgsValidator(
            CreateProductNameValidator createProductNameValidator,
            SellByTypeValidator sellByTypeValidator,
            RetailPriceValidator retailPriceValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        ) : base(
            sellByTypeValidator,
            retailPriceValidator,
            iUpsertMassProductArgsValidator
        )
        {
            Include(createProductNameValidator);
        }
    }
}
