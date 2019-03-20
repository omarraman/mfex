using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Readers;
using ZooFeedingCalculator.Repositories;

namespace ZooFeedingCalculatorTests
{
    [TestClass]
    public class RateRepositoryTestsValidData
    {
        private static Mock<IFileReader> _mockFileReader;
        private static RateRepository _rateRepository;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockFileReader.Setup(m => m.GetLines(It.IsAny<string>())).Returns(new String[]{"Lion;0.10;meat;",
                "Zebra;0.08;fruit;",
                    "Wolf;0.07;both;90%"});
            _rateRepository = new RateRepository(@"c:\temp\RateRepository.txt", _mockFileReader.Object);
        }

        [TestMethod]
        public void Constructor_ValidData_RatesPropertyHasCorrectCount()
        {
            Assert.AreEqual(3,_rateRepository.Rates.Count);
        }

        [TestMethod]
        public void Constructor_ValidData_RatesPropertyContainsExpectedRateObjects()
        {
            ValidateRate(_rateRepository.Rates.ElementAt(0), AnimalType.Lion, FoodConsumptionType.Meat, 1,.1M);
            ValidateRate(_rateRepository.Rates.ElementAt(1), AnimalType.Zebra, FoodConsumptionType.Fruit, 1,.08M);
            ValidateRate(_rateRepository.Rates.ElementAt(2), AnimalType.Wolf, FoodConsumptionType.Both, .9M,.07M);
        }

        private void ValidateRate(Rate rate ,AnimalType type,FoodConsumptionType foodConsumptionType,decimal? percentage,decimal rateValue)
        {
            Assert.AreEqual(type,rate.AnimalType);
            Assert.AreEqual(foodConsumptionType,rate.ConsumptionType);
            Assert.AreEqual(percentage,rate.Percentage);
            Assert.AreEqual(rateValue,rate.Value);
        }



        [TestMethod]
        public void GetRateForAnimalType_ValidType_ReturnsCorrectRate()
        {
            var rate = _rateRepository.GetRateForAnimalType(AnimalType.Zebra);
            ValidateRate(rate,AnimalType.Zebra,FoodConsumptionType.Fruit, 1,.08M);
        }

        [TestMethod]
        public void GetRateForAnimalType_InvalidType_ThrowsExpectedError()
        {
            Exception expectedException = null;

            try
            {
                _rateRepository.GetRateForAnimalType(AnimalType.Giraffe);
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);
            Assert.AreEqual("Cannot find rate for animalType Giraffe", expectedException.Message);

        }
    }
}
