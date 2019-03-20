using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;

namespace ZooFeedingCalculator.Repositories
{
    public class ZooRepository : IZooRepository
    {
        private readonly List<Animal> _animals = new List<Animal>();
        private XDocument _zooXDocument;

        public ZooRepository(string path,IXmlDocumentReader xmlDocumentReader)
        {
            SetXDocument(path, xmlDocumentReader);
            ProcessFile();
        }

        private void SetXDocument(string path, IXmlDocumentReader xmlDocumentReader)
        {
            _zooXDocument = xmlDocumentReader.GetXDocument(path);
        }

        private void ProcessFile()
        {
            foreach (var elementToBeRead in Enum.GetNames(typeof(AnimalType)))
            {
                ProcessElementsOfSameType(elementToBeRead);
            }
        }

        private void ProcessElementsOfSameType(string elementToBeRead)
        {
            var elements = _zooXDocument.Descendants(elementToBeRead);

            foreach (var element in elements)
            {
                var tempAnimal = MapElementToAnimal(element, elementToBeRead);
                _animals.Add(tempAnimal);
            }
        }

        private Animal MapElementToAnimal(XElement element, string elementToBeRead)
        {
            try
            {
                return new Animal
                {
                    Name = MapName(element),
                    Weight = MapWeight(element),
                    Type = MapAnimalType(elementToBeRead)
                };
            }
            catch (Exception)
            {
                throw new Exception($"Could not map element with attributes {string.Join("",element.Attributes().Select(o=>o.ToString()))}");
            }
        }

        private  AnimalType MapAnimalType(string element)
        {
            return (AnimalType)Enum.Parse(typeof(AnimalType),
                element);
        }

        private  decimal MapWeight(XElement element)
        {
            return Decimal.Parse(element.Attribute("kg").Value);
        }

        private  string MapName(XElement element)
        {
            return element.Attribute("name").Value;
        }

        public IEnumerable<Animal> GetAnimals()
        {
            return _animals;
        }
    }
}
