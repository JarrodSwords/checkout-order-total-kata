namespace PointOfSale.Implementations.Basic
{
    public class CreateProductArgsValidator : UpsertProductArgsValidator
    {
        public CreateProductArgsValidator(
            MassValidator massValidator,
            ProductMustNotExistValidator productMustNotExistValidator,
            RetailPriceValidator retailPriceValidator,
            SellByTypeValidator sellByTypeValidator
        ) : base(
            massValidator,
            retailPriceValidator,
            sellByTypeValidator
        )
        {
            Include(productMustNotExistValidator);
        }
    }
}
