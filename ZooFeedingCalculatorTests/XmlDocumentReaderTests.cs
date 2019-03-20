using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZooFeedingCalculator;
using ZooFeedingCalculator.Readers;

namespace ZooFeedingCalculatorTests
{
    [TestClass]
    public class XmlDocumentReaderTests
    {
        [TestMethod]
        public void GetXDocument_InvalidPath_ThrowsExpectedException()
        {
            var fileReader = new XmlDocumentReader();
            Exception expectedException = null;
            try
            {
                fileReader.GetXDocument(@"garbarge.txt");
            }
            catch (Exception e)
            {
                expectedException = e;
            }

            Assert.IsNotNull(expectedException);
            Assert.AreEqual(@"File specified by garbarge.txt does not exist",expectedException.Message);
        }

        [TestMethod]
        public void GetLines_EmptyFile_ThrowsExpectedException()
        {
            string filename = "empty.txt";
            using (File.Create(filename));
            var fileReader = new XmlDocumentReader();
            Exception expectedException = null;

            try
            {
                fileReader.GetXDocument(filename);
            }
            catch (Exception e)
            {
                expectedException = e;

            }
            File.Delete(filename);

            Assert.IsNotNull(expectedException);
            Assert.AreEqual(@"File specified by empty.txt is empty", expectedException.Message);

        }
    }
}
