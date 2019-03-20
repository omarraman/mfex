using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZooFeedingCalculator;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Repositories;

namespace ZooFeedingCalculatorTests
{
    [TestClass]
    public class ZooDailyFeedingCostCalculatorTests
    {
        private Mock<IPriceRepository> _mockPriceRepository;
        private Mock<IRateRepository> _mockRateRepository;
        private Mock<IZooRepository> _mockZooRepository;

        [TestMethod]
        public void CalculateCostPerDay_ValidData_ReturnsCorrectResult()
        {
            SetupMockPriceRepository();
            SetupMockRateRepository();
            SetupMockZooRepository();

            var zooCalc = new Mock<ZooDailyFeedingCostCalculator>(_mockRateRepository.Object, _mockPriceRepository.Object,
                _mockZooRepository.Object).Object;
            var costPerDay = zooCalc.CalculateCostPerDay();

            Assert.AreEqual(310.53744M
                , costPerDay);

        }

        private void SetupMockZooRepository()
        {
            _mockZooRepository = new Mock<IZooRepository>();
            var animalCollection = new List<Animal>
            {
                new Animal()
                {
                    Name = "Simba",
                    Type = AnimalType.Lion,
                    Weight = 160
                },
                new Animal()
                {
                    Name = "Chip",
                    Type = AnimalType.Zebra,
                    Weight = 100
                },
                new Animal()
                {
                    Name = "Pin",
                    Type = AnimalType.Wolf,
                    Weight = 78
                }
            };
            _mockZooRepository.Setup(m => m.GetAnimals()).Returns(animalCollection);
        }

        private void SetupMockRateRepository()
        {
            _mockRateRepository = new Mock<IRateRepository>();

            var lionRate = new Rate()
            {
                AnimalType = AnimalType.Lion,
                ConsumptionType = FoodConsumptionType.Meat,
                Percentage = 1,
                Value = .1M
            };
            var zebraRate = new Rate()
            {
                AnimalType = AnimalType.Zebra,
                ConsumptionType = FoodConsumptionType.Fruit,
                Percentage = 1,
                Value = .08M
            };
            var wolfRate = new Rate()
            {
                AnimalType = AnimalType.Wolf,
                ConsumptionType = FoodConsumptionType.Both,
                Percentage = .9M,
                Value = .07M
            };

            _mockRateRepository.Setup(m => m.GetRateForAnimalType(It.Is<AnimalType>(s => s == AnimalType.Lion)))
                .Returns(lionRate);
            _mockRateRepository.Setup(m => m.GetRateForAnimalType(It.Is<AnimalType>(s => s == AnimalType.Zebra)))
                .Returns(zebraRate);
            _mockRateRepository.Setup(m => m.GetRateForAnimalType(It.Is<AnimalType>(s => s == AnimalType.Wolf)))
                .Returns(wolfRate);
        }

        private void SetupMockPriceRepository()
        {
            _mockPriceRepository = new Mock<IPriceRepository>();
            _mockPriceRepository.Setup(m => m.GetPrice(It.Is<FoodType>(s => s == FoodType.Meat))).Returns(12.56M);
            _mockPriceRepository.Setup(m => m.GetPrice(It.Is<FoodType>(s => s == FoodType.Fruit))).Returns(5.6M);
        }
    }
}
