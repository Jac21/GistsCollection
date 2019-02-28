using System;
using ML.NET.Helpers;

namespace ML.NET
{
    class Program
    {
        static void Main()
        {
            var trainingDataLocation = @"Data/iris-data_training.csv";
            var testDataLocation = @"Data/iris-data_test.csv";

            var modelBuilder = new ModelBuilder(trainingDataLocation);
            var model = modelBuilder.BuildAndTrain();
            var accuracy = modelBuilder.Evaluate(model, testDataLocation);

            Console.WriteLine("*************************************************");
            Console.WriteLine($"*        Accuracy of the model : {accuracy}     *");
            Console.WriteLine("*************************************************");

            // Visualizing the results
            var testDataObjects = new IrisCsvReader().GetIrisDataFromCsv(testDataLocation);
            foreach (var iris in testDataObjects)
            {
                var prediction = model.Predict(iris);

                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Predicted type : {prediction.PredictedLabels}");
                Console.WriteLine($"Actual type :    {iris.Label}");
                Console.WriteLine("-------------------------------------------------");
            }

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}