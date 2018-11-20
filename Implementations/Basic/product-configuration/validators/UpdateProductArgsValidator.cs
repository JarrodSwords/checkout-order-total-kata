namespace PointOfSale.Implementations.Basic
{
    public class UpdateProductArgsValidator : UpsertProductArgsValidator
    {
        public UpdateProductArgsValidator(
            ProductMustExistValidator productMustExistValidator,
            SellByTypeValidator sellByTypeValidator,
            IUpsertEachesProductArgsValidator retailPriceValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        ) : base(
            sellByTypeValidator,
            retailPriceValidator,
            iUpsertMassProductArgsValidator
        )
        {
            Include(productMustExistValidator);
        }
    }
}
