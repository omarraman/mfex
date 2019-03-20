using ZooFeedingCalculator.Enums;

namespace ZooFeedingCalculator.Interfaces
{
    public interface IPriceRepository
    {
        decimal GetPrice(FoodType foodType);
    }
}
