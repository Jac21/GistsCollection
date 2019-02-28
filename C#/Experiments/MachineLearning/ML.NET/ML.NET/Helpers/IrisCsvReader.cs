using System.Collections.Generic;
using System.IO;
using System.Linq;
using ML.NET.Iris;

namespace ML.NET.Helpers
{
    public class IrisCsvReader
    {
        public IEnumerable<IrisFlower> GetIrisDataFromCsv(string dataLocation)
        {
            return File.ReadAllLines(dataLocation)
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => new IrisFlower
                {
                    SepalLength = float.Parse(x[0]),
                    SepalWidth = float.Parse(x[1]),
                    PentalLength = float.Parse(x[2]),
                    PentalWidth = float.Parse(x[3]),
                    Label = x[4]
                });
        }
    }
}