using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using ML.NET.Iris;

namespace ML.NET
{
    internal class ModelBuilder
    {
        private readonly string trainingDataLocation;

        public ModelBuilder(string trainingDataLocation)
        {
            this.trainingDataLocation = trainingDataLocation;
        }

        /// <summary>
        /// Using training data location that is passed trough constructor this method is building
        /// and training machine learning model.
        /// </summary>
        /// <returns></returns>
        public PredictionModel<IrisFlower, IrisPredict> BuildAndTrain()
        {
            // define tasks our model needs to do
            var pipeline = new LearningPipeline
            {
                // load data
                new TextLoader(trainingDataLocation).CreateFrom<IrisFlower>(true, ','),
                // format so ML can understand it
                new Dictionarizer("Label"),
                // gather attributes
                new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PentalLength", "PentalWidth"),
                // build the model
                new StochasticDualCoordinateAscentClassifier(),
                // convert encoded output back to string
                new PredictedLabelColumnOriginalValueConverter {PredictedLabelColumn = "PredictedLabel"}
            };

            return pipeline.Train<IrisFlower, IrisPredict>();
        }

        /// <summary>
        /// Using passed testing data and model, it calculates model's accuracy.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="testDataLocation"></param>
        /// <returns></returns>
        public double Evaluate(PredictionModel<IrisFlower, IrisPredict> model, string testDataLocation)
        {
            // load test data
            var testData = new TextLoader(testDataLocation).CreateFrom<IrisFlower>(true, ',');

            // load metric information about the model, accuracy
            var metrics = new ClassificationEvaluator().Evaluate(model, testData);
            return metrics.AccuracyMacro;
        }
    }
}