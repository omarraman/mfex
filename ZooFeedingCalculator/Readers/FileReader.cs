using System;
using System.IO;
using ZooFeedingCalculator.Interfaces;

namespace ZooFeedingCalculator.Readers
{
    public class FileReader : IFileReader
    {
        public string[] GetLines(string path)
        {
            if (File.Exists(path))
            {
                if (new FileInfo(path).Length==0)
                {
                    throw new Exception($"File specified by {path} is empty");
                }
                return File.ReadAllLines(path);
            }
            else
            {
                throw new Exception($"File specified by {path} does not exist");
            }
        }
    }
}
