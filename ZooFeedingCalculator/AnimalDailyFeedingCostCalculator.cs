using System;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;

namespace ZooFeedingCalculator
{
    public class AnimalDailyFeedingCostCalculator : IAnimalFeedingDailyCostCalculator
    {
        private readonly Animal _animal;
        private readonly IPriceRepository _priceRepository;
        private readonly Rate _rate;

        public AnimalDailyFeedingCostCalculator(Animal animal,IRateRepository rateRepository, IPriceRepository priceRepository)
        {
            _animal = animal;
            _priceRepository = priceRepository;
            _rate = rateRepository.GetRateForAnimalType(animal.Type);
        }

        public decimal GetDailyFoodCost()
        {
            return _animal.Weight * _rate.Value * GetPrice();
        }

        //public decimal GetDailyFoodCost(Animal animal)
        //{
        //    var rate = _rateRepository.GetRateForAnimalType(animal.Type);
        //    return animal.Weight * rate.Value * GetPrice(rate);
        //}

        private decimal GetPrice()
        {
            var meatPrice = _priceRepository.GetPrice(FoodType.Meat);
            var fruitPrice = _priceRepository.GetPrice(FoodType.Fruit);
            return _rate.ConsumptionType == FoodConsumptionType.Meat ? meatPrice :
                _rate.ConsumptionType == FoodConsumptionType.Fruit ? fruitPrice :
                GetPercentageCost(meatPrice, fruitPrice);

        }

        private decimal GetPercentageCost(decimal meatPrice, decimal fruitPrice)
        {
            return (decimal)_rate.Percentage * meatPrice + (1 - (decimal)_rate.Percentage) + fruitPrice;
        }
    }
}