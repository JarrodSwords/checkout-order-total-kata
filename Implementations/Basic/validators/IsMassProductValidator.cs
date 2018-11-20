using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class IsMassProductValidator : AbstractValidator<IProductNameArgs>
    {
        public IsMassProductValidator(IProductRepository productRepository)
        {
            When(x => productRepository.Exists(x.ProductName), () =>
            {
                RuleFor(x => x.ProductName)
                    .Must(x => productRepository.FindProduct(x).GetType() == typeof(MassProduct))
                    .WithMessage("{PropertyName} must correspond to a mass product");
            });
        }
    }
}
