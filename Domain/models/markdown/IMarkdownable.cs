using System;
using NodaMoney;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public interface IMarkdownable
    {
        Markdown Markdown { get; set; }

        Money GetMarkdownSalePrice();
        Money GetMarkdownSalePrice(Mass mass);
    }

    public abstract class Markdownable : IMarkdownable
    {
        public Markdown Markdown { get; set; }

        public abstract Money GetMarkdownSalePrice();

        public abstract Money GetMarkdownSalePrice(Mass mass);
    }

    public class MarkdownableAsEaches : Markdownable
    {
        public override Money GetMarkdownSalePrice() =>
            -Markdown.AmountOffRetail;

        public override Money GetMarkdownSalePrice(Mass mass) =>
            throw new NotImplementedException();
    }

    public class MarkdownableWithMass : Markdownable
    {
        public Mass Mass { get; set; }

        public MarkdownableWithMass()
        {
            Mass = new Mass(1, MassUnit.Pound);
        }

        public MarkdownableWithMass(double massAmount, string massUnit)
        {
            Mass = new Mass(
                massAmount,
                (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)
            );
        }

        public override Money GetMarkdownSalePrice() =>
            throw new NotImplementedException();

        public override Money GetMarkdownSalePrice(Mass mass) =>
            -Markdown.AmountOffRetail * (decimal) (mass / Mass);
    }
}
