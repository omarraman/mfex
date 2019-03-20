using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;

namespace ZooFeedingCalculator.Interfaces
{
    public interface IRateRepository
    {
        Rate GetRateForAnimalType(AnimalType animalType);

    }
}
