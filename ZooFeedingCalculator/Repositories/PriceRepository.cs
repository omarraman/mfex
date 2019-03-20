using System;
using System.IO;
using System.Linq;
using ZooFeedingCalculator.Entities;
using ZooFeedingCalculator.Enums;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Readers;

namespace ZooFeedingCalculator.Repositories
{
   public class PriceRepository : IPriceRepository
    {
        private readonly string[] _lines;

        public PriceRepository(string path,IFileReader reader)
        {
            _lines = reader.GetLines(path);
        }

        public decimal GetPrice(FoodType foodType)
        {
            string searchValue = foodType == FoodType.Fruit ? "Fruit" : "Meat";
            return Decimal.Parse(_lines.Single(m => m.StartsWith(searchValue)).Split('=')[1]);
        }
    }
}
