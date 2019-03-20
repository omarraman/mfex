using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Readers;

namespace ZooFeedingCalculator.Repositories
{
    public class RateRepository : IRateRepository
    {
        public List<Rate> Rates { get; } = new List<Rate>();
        private CultureInfo _cultureInfo;
        private TextInfo _textInfo;
        private readonly string[] _lines;

        public RateRepository(string path,IFileReader reader)
        {
            _lines = reader.GetLines(path);
            InitTextInfo();
            ProcessLines();
        }

        private void InitTextInfo()
        {
            _cultureInfo = Thread.CurrentThread.CurrentCulture;
            _textInfo = _cultureInfo.TextInfo;
        }

        private void ProcessLines()
        {
            foreach (var line in _lines)
            {
                Rates.Add(MapLineToRate(line));
            }
        }

        private Rate MapLineToRate(string line)
        {
            string[] lineArray = line.Split(';');

            try
            {
                return new Rate
                {
                    AnimalType = MapAnimalType(lineArray[0]),
                    ConsumptionType = MapConsumptionType(lineArray[2]),
                    Percentage = MapPercentageString(lineArray[3]),
                    Value = MapValue(lineArray[1])
                };
            }
            catch (Exception e)
            {
                throw new Exception($"This line '{line}' contained invalid data");
            }
        }

        private static decimal MapValue(string valueString)
        {
            return Decimal.Parse(valueString);
        }

        private FoodConsumptionType MapConsumptionType(string consumptionTypeString)
        {
            return (FoodConsumptionType)Enum.Parse(typeof(FoodConsumptionType),_textInfo.ToTitleCase(consumptionTypeString.ToLower()));
        }

        private static AnimalType MapAnimalType(string animalTypeString)
        {
            return (AnimalType)Enum.Parse(typeof(AnimalType), animalTypeString);
        }

        private decimal MapPercentageString(string percentageString)
        {
            if (percentageString.Trim()=="")
            {
                return 1;
            }
            else
            {
                return Decimal.Parse(percentageString.Replace("%", ""))/100;
            }
        }

        public Rate GetRateForAnimalType(AnimalType animalType)
        {
            if (Rates.Any(m=>m.AnimalType==animalType))
            {
                return Rates.Single(m => m.AnimalType == animalType);
            }
            else
            {
                //eg if the rates table does not include all the animals in the file
                throw new Exception($"Cannot find rate for animalType {animalType}");
            }
        }
    }



}
