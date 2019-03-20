namespace ZooFeedingCalculator.Interfaces
{
    public interface IFileReader
    {
        string[] GetLines(string path);
    }
}