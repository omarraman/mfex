using System;
using ZooFeedingCalculator.Enums;

namespace ZooFeedingCalculator.Entities
{
    public class Price
    {
        public FoodType Kind { get; set; }
        public Decimal Cost { get; set; }
    }
}
