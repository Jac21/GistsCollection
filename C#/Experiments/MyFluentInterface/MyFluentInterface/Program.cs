using System;
using MyFluentInterface.FluentBuilders;

namespace MyFluentInterface
{
    /// <summary>
    /// Boils down to a combination of the functional aspects of http://blogs.tedneward.com/patterns/Builder-CSharp/ 
    /// and the architected approach of https://scottlilly.com/how-to-create-a-fluent-interface-in-c/
    /// </summary>
    public class Program
    {
        static void Main()
        {
            //var director = new Director();
            //var product = director.Construct();

            //product.Parts.ForEach(Console.WriteLine);

            // Fluent Builder consumption

            var fluentBuilderFns = new FluentBuilderFns();

            var fluentProduct = fluentBuilderFns.Begin()
                .AddEngine()
                .AddSteeringWheel()
                .AddTires(4)
                .Where("ModelName").IsEqualTo("McClaren")
                .Build();

            fluentProduct.Parts.ForEach(Console.WriteLine);
        }
    }
}