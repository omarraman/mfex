using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;

namespace ZooFeedingCalculator
{
    public class ZooDailyFeedingCostCalculator
    {
        private readonly IRateRepository _rateRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IZooRepository _zooRepository;
        private decimal _meatPrice;
        private decimal _fruitPrice;

        public ZooDailyFeedingCostCalculator(IRateRepository rateRepository, IPriceRepository priceRepository,
            IZooRepository zooRepository)
        {
            _rateRepository = rateRepository;
            _priceRepository = priceRepository;
            _zooRepository = zooRepository;
        }

        public decimal CalculateCostPerDay()
        {
            var animals = _zooRepository.GetAnimals();

            GetPrices();

            return SumAnimalCost(animals);

        }

        private void GetPrices()
        {
            _meatPrice = _priceRepository.GetPrice(FoodType.Meat);
            _fruitPrice = _priceRepository.GetPrice(FoodType.Fruit);
        }

        private decimal SumAnimalCost(IEnumerable<Animal> animals)
        {
            decimal totalCost = 0M;
            foreach (var animal in animals)
            {
                totalCost += GetDailyFoodCost(animal);
            }
            return totalCost;
        }


        private decimal GetDailyFoodCost(Animal animal)
        {
            Rate rate = _rateRepository.GetRateForAnimalType(animal.Type);
            switch (rate.ConsumptionType)
            {
                case FoodConsumptionType.Fruit:
                    return animal.Weight * rate.Value * _fruitPrice;
                case FoodConsumptionType.Meat:
                    return animal.Weight * rate.Value * _meatPrice;
                default:
                    return animal.Weight * rate.Value * rate.Percentage * _meatPrice
                           + animal.Weight * rate.Value * (1 - rate.Percentage) * _fruitPrice;
            }
        }
    }
}
