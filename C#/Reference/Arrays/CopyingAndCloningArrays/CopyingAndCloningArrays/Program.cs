using System;

namespace CopyingAndCloningArrays
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("======================= Array.CopyTo =======================");

            var source = new[] {"Jeremy", "John", "Joseph"};
            var target = new string[4];

            // copies all the elements of the current array to the specified destination array,
            // first parameter being the array being copied to, and the second the index it should start at
            source.CopyTo(target, 1);
            foreach (var item in target)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("======================= Array.ConstrainedCopy =======================");

            var constrainedSource = new object[] {"Jeremy", "Joseph", 22};
            var constrainedTarget = new string[3];

            try
            {
                // guarantees that all changes are undone if the copy operation does not succeed completely because of some exception
                Array.ConstrainedCopy(constrainedSource, 0, constrainedTarget, 0, 3);
            }
            catch (ArrayTypeMismatchException)
            {
                Console.WriteLine(constrainedTarget[0] ?? "Nothing in this index");
                Console.WriteLine(constrainedTarget[1] ?? "Nor this one");
            }

            Console.WriteLine("======================= Array.ConvertAll =======================");

            var sourceToConvert = new[] {"Jeremy, converted", "John, converted", "Joseph, converted"};

            // used to convert an array of one type to an array of a different type, takes the source 
            // array as the first parameter, and then a second parameter of Converter<TInput,TOutput> delegate
            var convertedTarget = Array.ConvertAll(sourceToConvert, x => new Person(x));

            foreach (var item in convertedTarget)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("======================= Array.Clone =======================");

            var sourceToClone = new[] { "Jeremy, cloned", "John, cloned", "Joseph, cloned" };

            // does a shallow copy of all the elements in the source array and returns an object containing those elements
            var clonedTarget = (string[]) sourceToClone.Clone();

            foreach (var item in clonedTarget)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        public class Person
        {
            public string Name { get; set; }

            public Person(string name)
            {
                Name = name;
            }
        }
    }
}