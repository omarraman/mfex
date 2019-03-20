using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZooFeedingCalculator;
using ZooFeedingCalculator.Repositories;
using System.Xml.Linq;
using ZooFeedingCalculator.Interfaces;

namespace ZooFeedingCalculatorTests
{
    [TestClass]
    public class ZooRepositoryTests
    {

        [TestMethod]
        public void GetAnimals_ValidData_ContainsExpectedAnimals()
        {
            var document = new XDocument(
                                            new XElement("Zoo",
                                                new XElement("Lions",
                                                    new XElement("Lion",
                                                        new XAttribute("name", "Simba"),
                                                        new XAttribute("kg", 160)
                                                                )
                                                            ),
                                                new XElement("Tigers",
                                                    new XElement("Tiger",
                                                        new XAttribute("name", "Dante"),
                                                        new XAttribute("kg", 150)
                                                                )
                                                            )   
                                                        )
                                        );
            var mockFileReader = new Mock<IXmlDocumentReader>();
            mockFileReader.Setup(m => m.GetXDocument(It.IsAny<string>())).Returns(document);
            var zooRepository = new ZooRepository(@"c:\temp\RateRepository.txt", mockFileReader.Object);

            var animalsList = zooRepository.GetAnimals().ToList();

            var animal1 = animalsList.ElementAt(0);
            Assert.AreEqual("Simba",animal1.Name);
            Assert.AreEqual(160,animal1.Weight);

            var animal2 = animalsList.ElementAt(1);
            Assert.AreEqual("Dante", animal2.Name);
            Assert.AreEqual(150, animal2.Weight);

        }

        [TestMethod]
        public void Constructor_InvalidData_ThrowsExpectedException()
        {
            //give Simba a weight which is a string
            var document = new XDocument(
                new XElement("Zoo",
                    new XElement("Lions",
                        new XElement("Lion",
                            new XAttribute("name", "Simba"),
                            new XAttribute("kg", "xyz")
                        )
                    ),
                    new XElement("Tigers",
                        new XElement("Tiger",
                            new XAttribute("name", "Dante"),
                            new XAttribute("kg", 150)
                        )
                    )
                )
            );
            var mockFileReader = new Mock<IXmlDocumentReader>();
            mockFileReader.Setup(m => m.GetXDocument(It.IsAny<string>())).Returns(document);
            Exception expectedException = null;
            try
            {
                var zooRepository = new ZooRepository(@"c:\temp\RateRepository.txt", mockFileReader.Object);
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);
            Assert.AreEqual("Could not map element with attributes name=\"Simba\"kg=\"xyz\"", expectedException.Message);
        }
    }
}
