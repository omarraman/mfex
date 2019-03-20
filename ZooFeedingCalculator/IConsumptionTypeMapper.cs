namespace ZooFeedingCalculator
{
    public interface IConsumptionTypeMapper
    {
        FoodConsumptionType Map(string consumptionTypeString);
    }
}