using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using ZooFeedingCalculator;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Repositories;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Readers;

namespace ZooFeedingCalculatorTests
{
    [TestClass]
    public class PriceRepositoryTests
    {
        private static Mock<IFileReader> _mockFileReader;
        private static PriceRepository _repository;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockFileReader.Setup(m => m.GetLines(It.IsAny<string>())).Returns(new String[]{"Meat=12.56",
                "Fruit=5.60" });
            _repository = new PriceRepository(@"c:\temp\prices.txt", _mockFileReader.Object);
        }

        [TestMethod]
        public void GetPrice_FoodTypeMeat_ReturnsMeatPrice()
        {
            var returnedValue= _repository.GetPrice(FoodType.Meat);
            Assert.AreEqual(12.56M,returnedValue);
        }

        [TestMethod]
        public void GetPrice_FoodTypeFruit_ReturnsFruitPrice()
        {
            var returnedValue = _repository.GetPrice(FoodType.Fruit);
            Assert.AreEqual(5.6M, returnedValue);
        }
    }
}
