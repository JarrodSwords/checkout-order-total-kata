namespace PointOfSale.Implementations.Basic
{
    public class CreateProductArgsValidator : UpsertProductArgsValidator
    {
        public CreateProductArgsValidator(
            ProductMustNotExistValidator productMustNotExistValidator,
            SellByTypeValidator sellByTypeValidator,
            IUpsertEachesProductArgsValidator iUpsertEachesProductArgsValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        ) : base(
            sellByTypeValidator,
            iUpsertEachesProductArgsValidator,
            iUpsertMassProductArgsValidator
        )
        {
            Include(productMustNotExistValidator);
        }
    }
}
