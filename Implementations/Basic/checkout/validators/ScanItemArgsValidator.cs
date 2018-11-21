using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ScanItemArgsValidator : AbstractValidator<ScanItemArgs>
    {
        public ScanItemArgsValidator(
            IsEachesProductValidator isEachesProductValidator,
            OrderMustExistValidator orderMustExistValidator,
            ProductMustExistValidator productMustExistValidator
        )
        {
            Include(isEachesProductValidator);
            Include(orderMustExistValidator);
            Include(productMustExistValidator);
        }
    }
}
