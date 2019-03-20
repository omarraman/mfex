using System;
using System.IO;
using System.Xml.Linq;
using ZooFeedingCalculator.Interfaces;

namespace ZooFeedingCalculator.Readers
{
    public class XmlDocumentReader : IXmlDocumentReader
    {
        public XDocument GetXDocument(string path)
        {
            if (File.Exists(path))
            {
                if (new FileInfo(path).Length == 0)
                {
                    throw new Exception($"File specified by {path} is empty");
                }
                return XDocument.Load(path);

            }
            else
            {
                throw new Exception($"File specified by {path} does not exist");
            }
        }
    }
}
