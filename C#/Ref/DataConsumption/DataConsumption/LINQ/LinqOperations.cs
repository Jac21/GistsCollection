using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DataConsumption.LINQ
{
    // From the reference, page 297:
    // The standard query operators are: All, Any, Average, Cast, Count, Distinct, GroupBy, Join,
    // Max, Min, OrderBy, Select, SelectMany, Skip, SkipWhile, Sum, Take, Take-While, ThenBy,
    // Where
    public class LinqOperations
    {
        // class fields
        private readonly int[] _data;
        private readonly string[] _textData;

        /// <summary>
        /// constructor, initialize data array
        /// </summary>
        public LinqOperations()
        {
            _data = new[] {1, 2, 5, 6, 11};
            _textData = new[] {"AngularJS", "React", "Riot", "Mithril"};
        }

        /// <summary>
        /// LINQ Where Operator example
        /// </summary>
        public void WhereOperator()
        {
            // return values where those values are divisible by 2 and 
            // contain a remainder of 0
            var result = _data.Where(d => d%2 == 0);

            foreach (var i in result)
            {
                Console.WriteLine("LINQ Select: {0}", i);
            }
        }

        /// <summary>
        /// LINQ OrderBy Operator example
        /// </summary>
        public void OrderByOperator()
        {
            // order data array values that are greater than 5 in a descending fashion
            var result = _data.Where(d => d > 5).OrderByDescending(d => d);

            foreach (var i in result)
            {
                Console.WriteLine("LINQ Order By: {0}", i);
            }
        }

        /// <summary>
        /// LINQ to XML operation example
        /// </summary>
        /// <param name="xml"></param>
        public void LinqToXml(string xml)
        {
            // parse XML using LINQ, find descendents and attributes in XML
            XDocument doc = XDocument.Parse(xml);
            IEnumerable<string> personNames = from p in doc.Descendants("person")
                select (string) p.Attribute("firstname") + " " + (string) p.Attribute("lastname");

            foreach (string s in personNames)
            {
                Console.WriteLine("LINQ to XML: {0}", s);
            }
        }

        /// <summary>
        /// LINQ All Operator example
        /// </summary>
        public void AllOperator()
        {
            // check if all values in data array are less than
            // or equal to 2000
            bool result = _data.All(d => d <= 2000);

            Console.WriteLine("Linq All: {0}", result);
        }

        /// <summary>
        /// LINQ Any operator example
        /// </summary>
        public void AnyOperator()
        {
            // check if any value in data array contains a remainder
            // of 0 when divided by 2, return true if so
            bool result = _data.Any(d => d%2 == 0);

            Console.WriteLine("LINQ Any: {0}", result);
        }

        /// <summary>
        /// LINQ Average operator example
        /// </summary>
        public void AverageOperator()
        {
            // compute average of data array values
            double average = _data.Average();
            Console.WriteLine("LINQ Average Ints: {0}", average);

            // compute average of string array value lengths
            double textLengthAverage = _textData.Average(t => t.Length);
            Console.WriteLine("LINQ Average strings: {0}", textLengthAverage);
        }

        /// <summary>
        /// LINQ Cast operator example
        /// </summary>
        public void CastOperator()
        {
            // declare B classes
            B[] values = new B[3];
            values[0] = new B();
            values[1] = new B();
            values[2] = new B();

            // cast all object to a base type
            var result = values.Cast<A>();
            foreach (A value in result)
            {
                value.Y();
            }
        }

        /// <summary>
        /// LINQ Count operator example
        /// </summary>
        public void CountOperator()
        {
            // count number of elements greater than two
            int greaterThanTwo = _data.Count(d => d > 2);
            Console.WriteLine("LINQ Count: {0}", greaterThanTwo);
        }

        /// <summary>
        /// LINQ Distinct operator example
        /// </summary>
        public void DistinctOperator()
        {
            // invoke distinct on data array, set to enumerable integer object
            IEnumerable<int> distinctIntegers = _data.Distinct();

            // output values to console
            foreach (int value in distinctIntegers)
            {
                Console.WriteLine("LINQ Distinct: {0}", value);
            }
        }

        /// <summary>
        /// LINQ GroupBy operator example
        /// </summary>
        public void GroupByOperator()
        {
            // group elements by IsEven method call
            var result = _data.GroupBy(d => IsEven(d));

            // iterate over group results
            foreach (var group in result)
            {
                // output key from each group (false and true)
                Console.WriteLine("LINQ Distinct IsEven: {0}", group.Key);

                // output value for each group
                foreach (var value in group)
                {
                    Console.WriteLine("{0}", value);
                }

                // newline
                Console.WriteLine();
            }
        }

        
        static bool IsEven(int value)
        {
            return value%2 == 0;
        }

        /// <summary>
        /// LINQ Join operator example
        /// </summary>
        public void JoinOperator()
        {
            Console.WriteLine(string.Join(" is the only real frontend framework, \n", _textData));
        }
    }


    /*
     * Implementations
     */

    public static class LinqExtensions
    {
        // implementing Where
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }

    /*
     *  Example classes for use in LINQs
     */

    class A
    {
        public void Y()
        {
            Console.WriteLine("A.Y");
        }
    }

    class B : A
    {
        
    }
}
