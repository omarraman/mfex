using System.Collections.Generic;
using ZooFeedingCalculator.Entities;

namespace ZooFeedingCalculator.Interfaces
{
   public interface  IZooRepository
    {
        IEnumerable<Animal> GetAnimals();
    }
}
