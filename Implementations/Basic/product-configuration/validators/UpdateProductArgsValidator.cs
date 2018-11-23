namespace PointOfSale.Implementations.Basic
{
    public class UpdateProductArgsValidator : UpsertProductArgsValidator
    {
        public UpdateProductArgsValidator(
            MassValidator massValidator,
            ProductMustExistValidator productMustExistValidator,
            RetailPriceValidator retailPriceValidator,
            SellByTypeValidator sellByTypeValidator
        ) : base(
            massValidator,
            retailPriceValidator,
            sellByTypeValidator
        )
        {
            Include(productMustExistValidator);
        }
    }
}
