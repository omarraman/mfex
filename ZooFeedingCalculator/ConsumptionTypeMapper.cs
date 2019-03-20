using System;

namespace ZooFeedingCalculator
{
    public class ConsumptionTypeMapper : IConsumptionTypeMapper
    {
        public ConsumptionTypeMapper()
        {
        }

        public FoodConsumptionType Map(string consumptionTypeString)
        {
            switch (consumptionTypeString)
            {
                case "meat":
                    return FoodConsumptionType.Meat;
                case "fruit":
                    return FoodConsumptionType.Fruit;
                case "both":
                    return FoodConsumptionType.Both;
                default:
                    throw new Exception("Unknown food consumption type");
            }
        }
    }
}