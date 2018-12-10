using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace DayOne
{
    public class FrequencyCalibrator
    {
        private static int FrequencySummation =>
                System.IO.File
                    .ReadAllLines(@"C:\Users\jcant\Documents\GitHub\GistsCollection\AdventOfCode\2018\DayOne\input\input.txt")
                    .Select(x => int.Parse(x))
                    .ToArray()
                    .Sum();

        private static int FrequencyRepeated
        {
            get
            {
                List<int> frequencySums = new List<int>();

                var frequencyValues =
                    System.IO.File
                        .ReadAllLines(@"C:\Users\jcant\Documents\GitHub\GistsCollection\AdventOfCode\2018\DayOne\input\input-repeated.txt")
                        .Select(x => int.Parse(x))
                        .ToArray();

                int pointer;
                int currentFrequencySum = frequencyValues[0];

                for (pointer = 0; pointer <= frequencyValues.Length - 1; pointer++)
                {
                    // reset pointer accordingly, add starting value to sum
                    if (pointer == frequencyValues.Length - 1)
                    {
                        pointer = 0;
                        currentFrequencySum += frequencyValues[0];
                    }

                    currentFrequencySum += frequencyValues[pointer + 1];

                    if (frequencySums.Contains(currentFrequencySum))
                        return currentFrequencySum;
                    else
                        frequencySums.Add(currentFrequencySum);
                }

                return 0;
            }
        }

        static void Main()
        {
            Console.WriteLine($"Frequency summation value: {FrequencySummation}");
            Console.WriteLine($"Frequency repeated value: {FrequencyRepeated}");
        }
    }
}
