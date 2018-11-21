using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ScanWeightedItemArgsValidator : AbstractValidator<ScanWeightedItemArgs>
    {
        public ScanWeightedItemArgsValidator(
            IsMassProductValidator isMassProductValidator,
            OrderMustExistValidator orderMustExistValidator,
            ProductMustExistValidator productMustExistValidator,
            ScannedMassValidator scannedMassValidator
        )
        {
            Include(isMassProductValidator);
            Include(orderMustExistValidator);
            Include(productMustExistValidator);
            Include(scannedMassValidator);
        }
    }
}
