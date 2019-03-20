using System;
using ZooFeedingCalculator.Enums;

namespace ZooFeedingCalculator.Entities
{
   public class Animal
    {
        public AnimalType Type { get; set; }
        public string Name { get; set; }
        public Decimal Weight { get; set; }
    }
}
