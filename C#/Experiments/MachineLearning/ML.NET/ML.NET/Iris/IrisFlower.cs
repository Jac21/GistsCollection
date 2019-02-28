using Microsoft.ML.Runtime.Api;

namespace ML.NET.Iris
{
    public class IrisFlower
    {
        [Column("0")] public float SepalLength;

        [Column("1")] public float SepalWidth;

        [Column("2")] public float PentalLength;

        [Column("3")] public float PentalWidth;

        [Column("4")] [ColumnName("Label")] public string Label;
    }

    public class IrisPredict
    {
        [ColumnName("PredictedLabel")] public string PredictedLabels;
    }
}