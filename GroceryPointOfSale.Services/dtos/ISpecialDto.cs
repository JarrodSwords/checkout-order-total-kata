using System;

namespace GroceryPointOfSale.ApplicationServices
{
    public interface ISpecialDto
    {
        string Description { get; set; }
        DateTime EndTime { get; set; }
        bool IsActive { get; set; }
        int? Limit { get; set; }
        DateTime StartTime { get; set; }
    }
}
