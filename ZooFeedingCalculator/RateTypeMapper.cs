using System;

namespace ZooFeedingCalculator
{
    public class RateTypeMapper : IRateTypeMapper
    {
        public RateTypeMapper()
        {
        }

        public AnimalType Map(string animalTypeString)
        {
            switch (animalTypeString)
            {
                case "Lion":
                    return AnimalType.Lion;
                case "Giraffe":
                    return AnimalType.Giraffe;
                case "Piranha":
                    return AnimalType.Piranha;
                case "Wolf":
                    return AnimalType.Wolf;
                case "Tiger":
                    return AnimalType.Tiger;
                case "Zebra":
                    return AnimalType.Zebra;
                default:
                    throw new Exception("Unknown animal type");

            }

        }
    }
}