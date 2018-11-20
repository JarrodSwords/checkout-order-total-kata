namespace PointOfSale.Implementations.Basic
{
    public class CreateProductArgsValidator : UpsertProductArgsValidator
    {
        public CreateProductArgsValidator(
            ProductNameDoesNotExistValidator createProductNameValidator,
            SellByTypeValidator sellByTypeValidator,
            IUpsertEachesProductArgsValidator iUpsertEachesProductArgsValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        ) : base(
            sellByTypeValidator,
            iUpsertEachesProductArgsValidator,
            iUpsertMassProductArgsValidator
        )
        {
            Include(createProductNameValidator);
        }
    }
}
