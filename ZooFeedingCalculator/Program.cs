using System;
using Autofac;
using ZooFeedingCalculator.Interfaces;
using ZooFeedingCalculator.Readers;
using ZooFeedingCalculator.Repositories;

namespace ZooFeedingCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ConfigureContainer();
            var calculator = container.Resolve<ZooDailyFeedingCostCalculator>();
            var result = calculator.CalculateCostPerDay();
            Console.WriteLine($"The cost per day is {result}");
            Console.ReadKey();
        }

        static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ZooDailyFeedingCostCalculator>().AsSelf();
            builder.RegisterType<PriceRepository>().As<IPriceRepository>().WithParameter("path", "..\\..\\files\\prices.txt");
            builder.RegisterType<ZooRepository>().As<IZooRepository>().WithParameter("path", "..\\..\\files\\zoo.xml");
            builder.RegisterType<RateRepository>().As<IRateRepository>().WithParameter("path", "..\\..\\files\\rates.csv");
            builder.RegisterType<XmlDocumentReader>().As<IXmlDocumentReader>();
            builder.RegisterType<FileReader>().As<IFileReader>();


            return builder.Build();
        }
    }
}
