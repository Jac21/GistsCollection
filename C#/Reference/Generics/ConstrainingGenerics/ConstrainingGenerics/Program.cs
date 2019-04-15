using System;

namespace ConstrainingGenerics
{
    /// <summary>
    /// Constrain by Value Type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConstrainByValueType<T> where T : struct { }

    /// <summary>
    /// Constraint to Allow Only Reference Types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConstrainByReferenceType<T> where T : class { }

    interface IAnimal { }
    class Snake : IAnimal { }
    interface IMammal : IAnimal { }
    class Lion : IMammal { }
    class FuteLion : Lion { }

    /// <summary>
    /// Interface Type Constraint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConstrainByInterface<T> where T : IMammal { }

    /// <summary>
    /// Constrain by Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConstrainByClass<T> where T : Lion { }

    class ConstrainByParameterlessCtor<T> where T : new() { }

    /// <summary>
    /// Using Enum as Constraint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConstrainByEnum<T> where T : Enum
    {
        public void PrintValues()
        {
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
            {
                Console.WriteLine(Enum.GetName(typeof(T), item));
            }
        }
    }

    enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // usages of the above
            var constrainByValueType = new ConstrainByValueType<double>();

            var constrainByReferenceType = new ConstrainByReferenceType<ConstrainByValueType<double>>();

            var constrainByInterface = new ConstrainByInterface<Lion>();

            var constrainByClass = new ConstrainByClass<Lion>();

            var enumGeneric = new ConstrainByEnum<Rainbow>();
            enumGeneric.PrintValues();

            Console.ReadLine();
        }
    }
}