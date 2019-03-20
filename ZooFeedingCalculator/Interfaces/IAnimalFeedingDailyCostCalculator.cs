using ZooFeedingCalculator.Entities;

namespace ZooFeedingCalculator.Interfaces
{
    public interface IAnimalFeedingDailyCostCalculator
    {
        decimal GetDailyFoodCost();
    }
}