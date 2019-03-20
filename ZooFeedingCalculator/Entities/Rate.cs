using System;
using ZooFeedingCalculator.Enums;

namespace ZooFeedingCalculator.Entities
{
    public class Rate
    {
        public AnimalType AnimalType { get; set; }
        public Decimal Value { get; set; }
        public FoodConsumptionType  ConsumptionType { get; set; }
        public Decimal Percentage { get; set; }
    }
}
