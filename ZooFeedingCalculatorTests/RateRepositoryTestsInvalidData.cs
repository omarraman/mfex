using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZooFeedingCalculator;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Readers;
using ZooFeedingCalculator.Repositories;

namespace ZooFeedingCalculatorTests
{
    [TestClass]
    public class RateRepositoryTestsInvalidData
    {

        [TestMethod]
        public void Constructor_InvalidData_ThrowsExpectedError()
        {
            var mockFileReader = new Mock<IFileReader>();
            mockFileReader.Setup(m => m.GetLines(It.IsAny<string>())).Returns(new String[]{"Lion;xyz;meat;",
                "Zebra;0.08;fruit;",
                "Wolf;0.07;both;90%"});
            //simulate badd data in a line, like the rate value
            Exception expectedException = null;

            try
            {
                var rateRepository = new RateRepository(@"c:\temp\RateRepository.txt", mockFileReader.Object);
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            
            Assert.IsNotNull(expectedException);
            Assert.AreEqual("This line 'Lion;xyz;meat;' contained invalid data",expectedException.Message);


    }


}
}
