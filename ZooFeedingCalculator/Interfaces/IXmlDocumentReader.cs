using System.Xml.Linq;

namespace ZooFeedingCalculator.Interfaces
{
    public interface IXmlDocumentReader
    {
        XDocument GetXDocument(string path);
    }
}