using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class UpsertProductMarkdownArgsValidator : AbstractValidator<UpsertProductMarkdownArgs>
    {
        public UpsertProductMarkdownArgsValidator(
            AmountOffRetailValidator amountOffRetailValidator,
            ProductMustExistValidator productMustExistValidator,
            IProductRepository productRepository,
            SellByTypeValidator sellByTypeValidator,
            TemporalValidator temporalValidator
        )
        {
            Include(amountOffRetailValidator);
            Include(productMustExistValidator);
            Include(sellByTypeValidator);
            Include(temporalValidator);

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            When(x => productRepository.Exists(x.ProductName), () =>
            {
                RuleFor(x => x.AmountOffRetail)
                    .Must((args, x) => x <= productRepository.FindProduct(args.ProductName).RetailPrice)
                    .WithMessage("{PropertyName} must be less than or equal to 'Retail Price'")
                    .When(x => x.SellByType == "eaches");

                RuleFor(x => x.AmountOffRetail)
                    .Must((args, x) => x <= productRepository.FindProduct(args.ProductName).RetailPricePerUnit)
                    .WithMessage("{PropertyName} must be less than or equal to 'Retail Price Per Unit'")
                    .When(x => x.SellByType == "mass");
            });
        }
    }
}
