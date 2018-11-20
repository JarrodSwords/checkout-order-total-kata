using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class IsEachesProductValidator : AbstractValidator<IProductNameArgs>
    {
        public IsEachesProductValidator(IProductRepository productRepository)
        {
            When(x => productRepository.Exists(x.ProductName), () =>
            {
                RuleFor(x => x.ProductName)
                    .Must(x => productRepository.FindProduct(x).GetType() == typeof(EachesProduct))
                    .WithMessage("{PropertyName} must correspond to an eaches product");
            });
        }
    }
}
